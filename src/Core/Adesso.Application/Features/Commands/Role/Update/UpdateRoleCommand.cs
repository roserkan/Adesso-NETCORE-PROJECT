using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.Commands.Role.Update;

public class UpdateRoleCommand: IRequest<string>
{
    public UpdateRoleCommand(int id, string roleName)
    {
        Id = id;
        RoleName = roleName;
    }

    public int Id { get; set; }
    public string RoleName { get; set; }
}
