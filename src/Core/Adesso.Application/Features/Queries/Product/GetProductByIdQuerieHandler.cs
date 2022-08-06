using Adesso.Application.Constants;
using Adesso.Application.Dtos.Product;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.Product;

public class GetProductByIdQuerieHandler : IRequestHandler<GetProductByIdQuerie, IDataResult<ProductDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<ProductDto>> Handle(GetProductByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.Product>().GetByIdAsync(request.Id);

        var result = _mapper.Map<ProductDto>(category);

        if (result is null)
            return new ErrorDataResult<ProductDto>(Messages.ProductNotFound);

        return new SuccessDataResult<ProductDto>(result);
    }
}