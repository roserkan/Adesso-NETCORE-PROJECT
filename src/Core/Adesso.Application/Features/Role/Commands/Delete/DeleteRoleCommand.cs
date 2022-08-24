using Adesso.Application.Dtos.Role;
using MediatR;


namespace Adesso.Application.Features.Role.Commands.Delete;

public class DeleteRoleCommand : IRequest<DeletedRoleDto>
{
    public DeleteRoleCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
