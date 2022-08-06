using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.UserDetail;

public class GetUserDetailByIdQuerie : IRequest<IDataResult<UserDetailDto>>
{
    public int Id { get; set; }

    public GetUserDetailByIdQuerie(int id)
    {
        Id = id;
    }
}