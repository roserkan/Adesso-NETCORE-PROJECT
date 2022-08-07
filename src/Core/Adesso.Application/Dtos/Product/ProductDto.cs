

namespace Adesso.Application.Dtos.Product;

public class ProductDto: IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public DateTime CreatedDate { get; set; }
}
