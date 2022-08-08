using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Commands.MoneyPoint.Create;

public class CreateMoneyPointCommand: IRequest<string>
{
    public CreateMoneyPointCommand(int point, int categoryId)
    {
        Point = point;
        CategoryId = categoryId;
    }

    public int Point { get; set; }
    public int CategoryId { get; set; }
}
