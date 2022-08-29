using Adesso.Application.Dtos.UserDetail;
using MediatR;


namespace Adesso.Application.Features.UserDetail.Queries;

public class GetUserDetailByUserIdQuerie : IRequest<UserDetailDto>
{
    public int UserId { get; set; }

    public GetUserDetailByUserIdQuerie(int userId)
    {
        UserId = userId;
    }
}
