using Adesso.Application.Constants;
using Adesso.Application.Dtos.Product;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using Adesso.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.Product;

public class GetProductByIdQuerieHandler : IRequestHandler<GetProductByIdQuerie, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.Product>().GetByIdAsync(request.Id);

        var result = _mapper.Map<ProductDto>(category);

        if (result is null)
            throw new DatabaseValidationException(Messages.ProductNotFound);

        return result;
    }
}