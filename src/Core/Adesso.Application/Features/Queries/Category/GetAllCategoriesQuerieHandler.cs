using Adesso.Application.Dtos.Category;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.Category;

public class GetAllCategoriesQuerieHandler : IRequestHandler<GetAllCategoriesQuerie, IDataResult<List<CategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCategoriesQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<CategoryDto>>> Handle(GetAllCategoriesQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.GetRepository<Domain.Models.Category>().GetAll();

        var result = _mapper.Map<List<CategoryDto>>(categories);

        return new SuccessDataResult<List<CategoryDto>>(result);
    }
}