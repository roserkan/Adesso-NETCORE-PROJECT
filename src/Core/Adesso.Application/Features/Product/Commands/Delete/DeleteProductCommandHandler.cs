using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.Product;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Product.Commands.Delete;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeletedProductDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.Product> _productRepository;


    public DeleteProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _productRepository = _unitOfWork.GetRepository<Domain.Models.Product>();

    }

    public async Task<DeletedProductDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {

        await this.CheckProductExsist(request.Id);
   

        var product = _mapper.Map<Domain.Models.Product>(request);

        await _productRepository.DeleteAsync(product);
        var deletedProductDto = _mapper.Map<DeletedProductDto>(product);
        return deletedProductDto;
    }

    private async Task CheckProductExsist(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)  throw new BusinessException(Messages.ProductIdNotFound);

    }
}

