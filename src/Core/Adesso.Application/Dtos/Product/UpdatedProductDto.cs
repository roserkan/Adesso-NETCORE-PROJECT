

namespace Adesso.Application.Dtos.Product;

public class UpdatedProductDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
}



