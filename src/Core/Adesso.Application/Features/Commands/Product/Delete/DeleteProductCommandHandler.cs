using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;
namespace Adesso.Application.Features.Commands.Product.Delete;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, string>
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

    public async Task<string> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(await CheckProductExsist(request.Id));
   

        var product = _mapper.Map<Domain.Models.Product>(request);

        var rows = await _productRepository.DeleteAsync(product);

        return Messages.ProductDeleted;
    }

    private async Task<IResult> CheckProductExsist(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return new ErrorResult(Messages.ProductIdNotFound);
        }
        return new SuccessResult();
    }
}

