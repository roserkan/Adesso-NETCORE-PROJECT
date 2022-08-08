using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.UserDetail.Commands.Delete;

public class DeleteUserDetailCommand : IRequest<string>
{
    public DeleteUserDetailCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}