using Adesso.Application.Dtos.Order;
using Adesso.Application.Dtos.OrderItem;
using MediatR;


namespace Adesso.Application.Features.Order.Commands.Create;

public class CreateOrderCommand: IRequest<CreatedOrderDto>
{
    public int UserId { get; set; }
    public List<CreateOrderItemDto> CreateOrderItemDtos { get; set; }
}
