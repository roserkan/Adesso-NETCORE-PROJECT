using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.User.Commands.Delete;

public class DeleteUserCommand : IRequest<string>
{
    public DeleteUserCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}