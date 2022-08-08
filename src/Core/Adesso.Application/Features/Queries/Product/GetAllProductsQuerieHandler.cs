using Adesso.Application.Dtos.Product;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.Product;

public class GetAllProductsQuerieHandler : IRequestHandler<GetAllProductsQuerie, List<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.Product> _productRepository;

    public GetAllProductsQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productRepository = _unitOfWork.GetRepository<Domain.Models.Product>();
    }

    public async Task<List<ProductDto>> Handle(GetAllProductsQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _productRepository.GetAll();

        var result = _mapper.Map<List<ProductDto>>(categories);

        return result;
    }
}