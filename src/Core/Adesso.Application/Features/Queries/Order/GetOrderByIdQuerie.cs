using Adesso.Application.Dtos.Order;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.Order;

public class GetOrderByIdQuerie : IRequest<OrderDto>
{
    public int Id { get; set; }

    public GetOrderByIdQuerie(int id)
    {
        Id = id;
    }
}