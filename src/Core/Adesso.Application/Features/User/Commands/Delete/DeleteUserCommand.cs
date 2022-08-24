using Adesso.Application.Dtos.User;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.User.Commands.Delete;

public class DeleteUserCommand : IRequest<DeletedUserDto>
{
    public DeleteUserCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}