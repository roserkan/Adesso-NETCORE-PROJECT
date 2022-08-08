using Adesso.Application.Dtos.Category;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.Category;

public class GetAllCategoriesQuerieHandler : IRequestHandler<GetAllCategoriesQuerie, List<CategoryDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.Category> _categoryRepository;


    public GetAllCategoriesQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _categoryRepository = unitOfWork.GetRepository<Domain.Models.Category>();
    }

    public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAll();

        var result = _mapper.Map<List<CategoryDto>>(categories);

        return result;
    }
}