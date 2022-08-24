using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.Product;
using Adesso.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Product.Queries;

public class GetProductByIdQuerieHandler : IRequestHandler<GetProductByIdQuerie, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.Product> _productRepository;


    public GetProductByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productRepository = _unitOfWork.GetRepository<Domain.Models.Product>();
    }

    public async Task<ProductDto> Handle(GetProductByIdQuerie request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        var result = _mapper.Map<ProductDto>(product);

        if (result is null) throw new BusinessException(Messages.ProductNotFound);

        return result;
    }
}