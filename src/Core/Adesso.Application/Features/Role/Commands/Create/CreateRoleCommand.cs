using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Role.Commands.Create;

public class CreateRoleCommand: IRequest<string>
{
 
    public string RoleName { get; set; }

    public CreateRoleCommand(string roleName)
    {
        RoleName = roleName;
    }
}
