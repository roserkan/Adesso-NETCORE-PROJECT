using Adesso.Application.Constants;
using Adesso.Application.Dtos.Category;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.Category;

public class GetCategoryByIdQuerieHandler : IRequestHandler<GetCategoryByIdQuerie, IDataResult<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<CategoryDto>> Handle(GetCategoryByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.Category>().GetByIdAsync(request.Id);

        var result = _mapper.Map<CategoryDto>(category);

        if (result is null)
            return new ErrorDataResult<CategoryDto>(Messages.CategoryIdNotFound);

        return new SuccessDataResult<CategoryDto>(result);
    }
}