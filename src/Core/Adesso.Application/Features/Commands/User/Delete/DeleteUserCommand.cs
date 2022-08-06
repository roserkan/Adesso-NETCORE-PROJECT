using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Commands.User.Delete;

public class DeleteUserCommand : IRequest<IDataResult<DeleteUserCommand>>
{
    public DeleteUserCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}