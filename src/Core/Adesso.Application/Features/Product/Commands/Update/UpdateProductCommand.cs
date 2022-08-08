using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.Product.Commands.Update;

public class UpdateProductCommand : IRequest<string>
{
    public UpdateProductCommand(int id, string name, string imagePath, double price, int categoryId, int stock)
    {
        Id = id;
        Name = name;
        ImagePath = imagePath;
        Price = price;
        CategoryId = categoryId;
        Stock = stock;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public int Stock { get; set; }
}
