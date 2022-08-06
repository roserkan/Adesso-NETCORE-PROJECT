

namespace Adesso.Application.Dtos.OrderItem;

public class OrderItemDto : IDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

}

