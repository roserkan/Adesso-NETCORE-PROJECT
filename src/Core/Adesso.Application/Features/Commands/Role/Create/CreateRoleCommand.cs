using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Commands.Role.Create;

public class CreateRoleCommand: IRequest<IDataResult<CreateRoleCommand>>
{
 
    public string RoleName { get; set; }

    public CreateRoleCommand(string roleName)
    {
        RoleName = roleName;
    }
}
