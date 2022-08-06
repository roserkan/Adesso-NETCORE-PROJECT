using Adesso.Application.Dtos.User;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.User;

public class GetAllUsersQuerie : IRequest<IDataResult<List<UserDto>>>
{
}
