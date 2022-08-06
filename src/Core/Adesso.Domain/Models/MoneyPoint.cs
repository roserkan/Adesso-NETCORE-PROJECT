

namespace Adesso.Domain.Models;

public class MoneyPoint: BaseEntity
{
    public int Point { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}