using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.Product;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Product.Commands.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdatedProductDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.Product> _productRepository;
    private IGenericRepository<Domain.Models.Category> _categoryRepository;

    public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _productRepository = _unitOfWork.GetRepository<Domain.Models.Product>();
        _categoryRepository = _unitOfWork.GetRepository<Domain.Models.Category>();
    }

    public async Task<UpdatedProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        await this.CheckProductExist(request.Id);
        await this.CheckProductNameExist(request.Name);
        await this.CheckCategoryExist(request.CategoryId);

        var product = _mapper.Map<Domain.Models.Product>(request);

        await _unitOfWork.GetRepository<Domain.Models.Product>().UpdateAsync(product);
        var updatedProductDto = _mapper.Map<UpdatedProductDto>(product);
        return updatedProductDto;
    }


    private async Task CheckProductExist(int id)
    {
        var product = await _unitOfWork.GetRepository<Domain.Models.Product>().GetByIdAsync(id);
        if (product is null) throw new BusinessException(Messages.ProductNotFound);

    }

    private async Task CheckProductNameExist(string name)
    {
        var product = await _unitOfWork.GetRepository<Domain.Models.Product>()
            .GetSingleAsync(p => p.Name == name);
        if (product is not null) throw new BusinessException(Messages.ProductNameAlreadyExist);
    }
    private async Task CheckCategoryExist(int categoryId)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.Category>().GetByIdAsync(categoryId);
        if (category is null) throw new BusinessException(Messages.CategoryIdNotFound);
    }

}