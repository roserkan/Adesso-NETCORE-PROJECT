using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.MoneyPoint.Commands.Update;

public class UpdateMoneyPointCommand: IRequest<DeletedMoneyPointDto>
{
    public UpdateMoneyPointCommand(int id, int point, int categoryId)
    {
        Id = id;
        Point = point;
        CategoryId = categoryId;
    }

    public int Id { get; set; }
    public int Point { get; set; }
    public int CategoryId { get; set; }
}
