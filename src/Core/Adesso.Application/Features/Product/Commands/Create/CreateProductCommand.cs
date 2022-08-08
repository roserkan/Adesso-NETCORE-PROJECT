using MediatR;

namespace Adesso.Application.Features.Product.Commands.Create;

public class CreateProductCommand: IRequest<string>
{
    public CreateProductCommand(string name, string imagePath, double price, int categoryId, int stock)
    {
        Name = name;
        ImagePath = imagePath;
        Price = price;
        CategoryId = categoryId;
        Stock = stock;
    }

    public string Name { get; set; }
    public string ImagePath { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public int Stock { get; set; }
}
