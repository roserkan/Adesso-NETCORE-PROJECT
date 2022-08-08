using Adesso.Application.Dtos.OrderItem;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.OrderItem.Queries;

public class GetAllOrderItemsQuerie : IRequest<List<OrderItemDto>>
{
}
