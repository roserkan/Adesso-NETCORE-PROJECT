

namespace Adesso.Domain.Models;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }

    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }
}
