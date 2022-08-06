using Adesso.Application.Dtos.Product;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.Product;

public class GetAllProductsQuerieHandler : IRequestHandler<GetAllProductsQuerie, IDataResult<List<ProductDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllProductsQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<ProductDto>>> Handle(GetAllProductsQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.GetRepository<Domain.Models.Product>().GetAll();

        var result = _mapper.Map<List<ProductDto>>(categories);

        return new SuccessDataResult<List<ProductDto>>(result);
    }
}