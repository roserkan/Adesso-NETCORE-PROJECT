using Adesso.Application.Dtos.OrderItem;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.OrderItem;

public class GetAllOrderItemsQuerie : IRequest<IDataResult<List<OrderItemDto>>>
{
}
