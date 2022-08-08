using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.Product.Commands.Delete;

public class DeleteProductCommand : IRequest<string>
{
    public DeleteProductCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
