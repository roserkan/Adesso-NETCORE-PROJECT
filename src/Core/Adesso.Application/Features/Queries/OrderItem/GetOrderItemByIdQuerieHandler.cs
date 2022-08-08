using Adesso.Application.Constants;
using Adesso.Application.Dtos.OrderItem;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using Adesso.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.OrderItem;

public class GetOrderItemByIdQuerieHandler : IRequestHandler<GetOrderItemByIdQuerie, OrderItemDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderItemByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderItemDto> Handle(GetOrderItemByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.OrderItem>().GetByIdAsync(request.Id);

        var result = _mapper.Map<OrderItemDto>(category);

        if (result is null)
            throw new DatabaseValidationException(Messages.OrderItemNotFound);

        return result;
    }
}