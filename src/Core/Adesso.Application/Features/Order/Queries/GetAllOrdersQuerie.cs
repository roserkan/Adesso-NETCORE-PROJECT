using Adesso.Application.Dtos.Order;
using MediatR;

namespace Adesso.Application.Features.Order.Queries;

public class GetAllOrdersQuerie : IRequest<List<OrderDto>>
{
}
