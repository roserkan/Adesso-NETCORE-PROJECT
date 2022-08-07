

namespace Adesso.Application.Dtos.OrderItem;

public class CreateOrderItemDto : IDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

}


