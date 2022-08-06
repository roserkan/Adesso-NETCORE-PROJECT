using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.Product.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IDataResult<CreateProductCommand>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IDataResult<CreateProductCommand>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(
                await CheckCategoryExist(request.CategoryId),
                await CheckProductNameExist(request.Name)
            );
        if (result != null)
        {
            return new ErrorDataResult<CreateProductCommand>(result.Message);
        }

        var category = _mapper.Map<Domain.Models.Product>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.Product>().AddAsync(category);

        return new SuccessDataResult<CreateProductCommand>(request, Messages.ProductCreated);
    }


    private async Task<IResult> CheckProductNameExist(string name)
    {
        var product = await _unitOfWork.GetRepository<Domain.Models.Product>()
            .GetSingleAsync(p => p.Name == name);
        if (product is not null)
        {
            return new ErrorResult(Messages.ProductNameAlreadyExist);
        }
        return new SuccessResult();
    }
    private async Task<IResult> CheckCategoryExist(int categoryId)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.Category>().GetByIdAsync(categoryId);
        if (category is null)
        {
            return new ErrorResult(Messages.CategoryIdNotFound);
        }
        return new SuccessResult();
    }




}
