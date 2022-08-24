using Adesso.Application.Dtos.Product;
using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.Product.Commands.Delete;

public class DeleteProductCommand : IRequest<DeletedProductDto>
{
    public DeleteProductCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
