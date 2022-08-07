using Adesso.Application.Dtos.OrderItem;
using Adesso.Application.Dtos.Product;
using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.Commands.Order.Create;

public class CreateOrderCommand: IRequest<IDataResult<CreateOrderCommand>>
{
    public int UserId { get; set; }
    public List<CreateOrderItemDto> CreateOrderItemDtos { get; set; }
}
