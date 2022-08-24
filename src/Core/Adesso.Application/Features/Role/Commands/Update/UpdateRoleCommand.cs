using Adesso.Application.Dtos.Role;
using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.Role.Commands.Update;

public class UpdateRoleCommand: IRequest<UpdatedRoleDto>
{
    public UpdateRoleCommand(int id, string roleName)
    {
        Id = id;
        RoleName = roleName;
    }

    public int Id { get; set; }
    public string RoleName { get; set; }
}
