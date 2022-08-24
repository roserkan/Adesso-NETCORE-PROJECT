using Adesso.Application.Dtos.Role;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Role.Commands.Create;

public class CreateRoleCommand: IRequest<CreatedRoleDto>
{
 
    public string RoleName { get; set; }

    public CreateRoleCommand(string roleName)
    {
        RoleName = roleName;
    }
}
