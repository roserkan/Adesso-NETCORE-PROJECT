using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.MoneyPoint.Queries;

public class GetMoneyPointByIdQuerie : IRequest<MoneyPointDto>
{
    public int Id { get; set; }

    public GetMoneyPointByIdQuerie(int id)
    {
        Id = id;
    }
}