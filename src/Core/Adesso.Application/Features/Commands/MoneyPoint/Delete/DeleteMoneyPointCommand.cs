using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.Commands.MoneyPoint.Delete;

public class DeleteMoneyPointCommand : IRequest<IDataResult<DeleteMoneyPointCommand>>
{
    public DeleteMoneyPointCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
