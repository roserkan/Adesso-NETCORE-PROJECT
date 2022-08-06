using Adesso.Application.Dtos.OrderItem;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.OrderItem;

public class GetOrderItemByIdQuerie : IRequest<IDataResult<OrderItemDto>>
{
    public int Id { get; set; }

    public GetOrderItemByIdQuerie(int id)
    {
        Id = id;
    }
}