using Adesso.Application.Dtos.Role;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.Role;

public class GetAllRolesQuerie : IRequest<List<RoleDto>>
{
}
