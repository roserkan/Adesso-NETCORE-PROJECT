namespace Adesso.Domain.Models;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public MoneyPoint MoneyPoint { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
