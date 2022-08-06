using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;
namespace Adesso.Application.Features.Commands.Product.Delete;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, IDataResult<DeleteProductCommand>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IDataResult<DeleteProductCommand>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(await CheckProductExsist(request.Id));
        if (result != null)
        {
            return new ErrorDataResult<DeleteProductCommand>(result.Message);
        }

        var product = _mapper.Map<Domain.Models.Product>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.Product>().DeleteAsync(product);

        return new SuccessDataResult<DeleteProductCommand>(request, Messages.ProductDeleted);
    }

    private async Task<IResult> CheckProductExsist(int id)
    {
        var product = await _unitOfWork.GetRepository<Domain.Models.Product>().GetByIdAsync(id);
        if (product is null)
        {
            return new ErrorResult(Messages.ProductIdNotFound);
        }
        return new SuccessResult();
    }
}

