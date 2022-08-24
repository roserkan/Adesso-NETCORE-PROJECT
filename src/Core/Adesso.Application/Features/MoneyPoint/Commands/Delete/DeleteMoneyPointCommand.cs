using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.MoneyPoint.Commands.Delete;

public class DeleteMoneyPointCommand : IRequest<DeletedMoneyPointDto>
{
    public DeleteMoneyPointCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
