using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.Role.Commands.Delete;

public class DeleteRoleCommand : IRequest<string>
{
    public DeleteRoleCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
