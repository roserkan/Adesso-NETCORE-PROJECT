using Adesso.Application.Dtos.Order;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.Order;

public class GetAllOrdersQuerie : IRequest<IDataResult<List<OrderDto>>>
{
}
