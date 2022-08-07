
namespace Adesso.Application.Dtos.Order;

public class OrderDto : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime CreatedDate { get; set; }
}

