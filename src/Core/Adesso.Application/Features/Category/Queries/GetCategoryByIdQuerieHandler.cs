using Adesso.Application.Constants;
using Adesso.Application.Dtos.Category;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using Adesso.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Category.Queries;

public class GetCategoryByIdQuerieHandler : IRequestHandler<GetCategoryByIdQuerie, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.Category> _categoryRepository;


    public GetCategoryByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _categoryRepository = unitOfWork.GetRepository<Domain.Models.Category>();

    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);

        var result = _mapper.Map<CategoryDto>(category);

        if (result is null)
            throw new DatabaseValidationException(Messages.CategoryIdNotNull);

        return result;
    }
}