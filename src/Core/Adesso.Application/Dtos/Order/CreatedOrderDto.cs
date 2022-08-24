
namespace Adesso.Application.Dtos.Order;

public class CreatedOrderDto : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Total { get; set; }
}

