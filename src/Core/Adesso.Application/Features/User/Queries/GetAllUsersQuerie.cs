using Adesso.Application.Dtos.User;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.User.Queries;

public class GetAllUsersQuerie : IRequest<List<UserDto>>
{
}
