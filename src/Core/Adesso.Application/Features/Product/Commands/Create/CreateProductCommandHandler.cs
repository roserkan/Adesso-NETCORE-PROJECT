using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.Product;
using Adesso.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Product.Commands.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatedProductDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.Product> _productRepository;
    private IGenericRepository<Domain.Models.Category> _categoryRepository;


    public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _productRepository = _unitOfWork.GetRepository<Domain.Models.Product>();
        _categoryRepository = _unitOfWork.GetRepository<Domain.Models.Category>();

    }

    public async Task<CreatedProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await this.CheckCategoryExist(request.CategoryId);
        await this.CheckProductNameExist(request.Name);


        var product = _mapper.Map<Domain.Models.Product>(request);
        await _productRepository.AddAsync(product);
        var createdProductDto = _mapper.Map<CreatedProductDto>(product);
        return createdProductDto;
    }


    private async Task CheckProductNameExist(string name)
    {
        var product = await _productRepository
            .GetSingleAsync(p => p.Name == name);
        if (product is not null) throw new BusinessException(Messages.ProductNameAlreadyExist);

    }
    private async Task CheckCategoryExist(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category is null) throw new BusinessException(Messages.CategoryIdNotFound);

    }




}
