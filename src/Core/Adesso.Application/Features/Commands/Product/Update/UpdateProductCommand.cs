using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.Commands.Product.Update;

public class UpdateProductCommand : IRequest<IDataResult<UpdateProductCommand>>
{
    public UpdateProductCommand(int id, string name, string imagePath, double price, int categoryId)
    {
        Id = id;
        Name = name;
        ImagePath = imagePath;
        Price = price;
        CategoryId = categoryId;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
}
