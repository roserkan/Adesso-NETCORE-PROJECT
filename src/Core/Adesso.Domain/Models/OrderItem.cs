

namespace Adesso.Domain.Models;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
    public int Quantity { get; set; }

}
