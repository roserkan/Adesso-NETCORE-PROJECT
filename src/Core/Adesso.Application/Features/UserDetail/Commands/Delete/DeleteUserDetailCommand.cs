using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.UserDetail.Commands.Delete;

public class DeleteUserDetailCommand : IRequest<DeletedUserDetailDto>
{
    public DeleteUserDetailCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}