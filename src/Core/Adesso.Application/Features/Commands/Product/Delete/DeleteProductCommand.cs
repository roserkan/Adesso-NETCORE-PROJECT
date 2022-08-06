using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.Commands.Product.Delete;

public class DeleteProductCommand : IRequest<IDataResult<DeleteProductCommand>>
{
    public DeleteProductCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
