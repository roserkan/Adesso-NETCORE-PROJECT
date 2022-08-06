using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Commands.Category.Delete;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, IDataResult<DeleteCategoryCommand>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<DeleteCategoryCommand>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(await CheckCategoryExsist(request.Id));
        if (result != null)
        {
            return new ErrorDataResult<DeleteCategoryCommand>(result.Message);
        }

        var category = _mapper.Map<Domain.Models.Category>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.Category>().DeleteAsync(category);

        return new SuccessDataResult<DeleteCategoryCommand>(request, Messages.CategoryDeleted);
    }

    private async Task<IResult> CheckCategoryExsist(int id)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.Category>().GetByIdAsync(id);
        if (category is null)
        {
            return new ErrorResult(Messages.CategoryIdNotFound);
        }
        return new SuccessResult();
    }

}
