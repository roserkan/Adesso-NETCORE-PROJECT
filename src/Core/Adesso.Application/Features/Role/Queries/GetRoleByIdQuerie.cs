using Adesso.Application.Dtos.Role;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Role.Queries;

public class GetRoleByIdQuerie : IRequest<RoleDto>
{
    public int Id { get; set; }

    public GetRoleByIdQuerie(int id)
    {
        Id = id;
    }
}