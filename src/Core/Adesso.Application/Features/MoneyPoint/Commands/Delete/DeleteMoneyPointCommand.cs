using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.MoneyPoint.Commands.Delete;

public class DeleteMoneyPointCommand : IRequest<string>
{
    public DeleteMoneyPointCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
