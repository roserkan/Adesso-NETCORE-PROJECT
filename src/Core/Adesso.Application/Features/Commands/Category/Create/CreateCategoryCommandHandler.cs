using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.Category.Create;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, IDataResult<CreateCategoryCommand>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<CreateCategoryCommand>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(await CheckCategoryNameExist(request.Name));
        if (result != null)
        {
            return new ErrorDataResult<CreateCategoryCommand>(result.Message);
        }

        var category = _mapper.Map<Domain.Models.Category>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.Category>().AddAsync(category);

        return new SuccessDataResult<CreateCategoryCommand>(request, Messages.CategoryCreated);
    }

    private async Task<IResult> CheckCategoryNameExist(string name)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.Category>().GetSingleAsync(c => c.Name == name);
        if (category is not null)
        {
            return new ErrorResult(Messages.CategoryNameAlreadyExist);
        }
        return new SuccessResult();
    }

}