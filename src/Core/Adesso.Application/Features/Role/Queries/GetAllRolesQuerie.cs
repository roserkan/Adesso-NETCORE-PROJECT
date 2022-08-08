using Adesso.Application.Dtos.Role;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Role.Queries;

public class GetAllRolesQuerie : IRequest<List<RoleDto>>
{
}
