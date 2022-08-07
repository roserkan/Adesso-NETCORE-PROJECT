

namespace Adesso.Domain.Models;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
}
