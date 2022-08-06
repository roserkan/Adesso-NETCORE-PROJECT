using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.Product.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, IDataResult<UpdateProductCommand>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IDataResult<UpdateProductCommand>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(
                await CheckProductExist(request.Id),
                await CheckProductNameExist(request.Name),
                await CheckCategoryExist(request.CategoryId)
            );
        if (result != null)
        {
            return new ErrorDataResult<UpdateProductCommand>(result.Message);
        }

        var product = _mapper.Map<Domain.Models.Product>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.Product>().UpdateAsync(product);

        return new SuccessDataResult<UpdateProductCommand>(request, Messages.ProductUpdated);
    }


    private async Task<IResult> CheckProductExist(int id)
    {
        var product = await _unitOfWork.GetRepository<Domain.Models.Product>().GetByIdAsync(id);
        if (product is null)
        {
            return new ErrorResult(Messages.ProductNotFound);
        }
        return new SuccessResult();
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