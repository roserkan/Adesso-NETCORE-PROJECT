using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.UserDetail;

public class GetAllUserDetailsQuerie : IRequest<List<UserDetailDto>>
{
}
