using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Category.Commands.Create;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.Category> _categoryRepository;
    

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _categoryRepository = unitOfWork.GetRepository<Domain.Models.Category>();
    }

    public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(await CheckCategoryNameExist(request.Name));

        var category = _mapper.Map<Domain.Models.Category>(request);

        var rows = await _categoryRepository.AddAsync(category);

        return Messages.CategoryCreated;
    }

    private async Task<IResult> CheckCategoryNameExist(string name)
    {
        var category = await _categoryRepository.GetSingleAsync(c => c.Name == name);
        if (category is not null)
        {
            return new ErrorResult(Messages.CategoryNameAlreadyExist);
        }
        return new SuccessResult();
    }

}