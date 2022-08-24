using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.MoneyPoint.Commands.Create;

public class CreateMoneyPointCommand: IRequest<CreatedMoneyPointDto>
{
    public CreateMoneyPointCommand(int point, int categoryId)
    {
        Point = point;
        CategoryId = categoryId;
    }

    public int Point { get; set; }
    public int CategoryId { get; set; }
}
