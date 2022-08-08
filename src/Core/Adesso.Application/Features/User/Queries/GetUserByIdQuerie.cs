using Adesso.Application.Dtos.User;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.User.Queries;

public class GetUserByIdQuerie : IRequest<UserDto>
{
    public int Id { get; set; }

    public GetUserByIdQuerie(int id)
    {
        Id = id;
    }
}