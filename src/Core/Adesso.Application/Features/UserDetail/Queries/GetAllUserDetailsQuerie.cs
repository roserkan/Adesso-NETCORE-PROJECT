using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.UserDetail.Queries;

public class GetAllUserDetailsQuerie : IRequest<List<UserDetailDto>>
{
}
