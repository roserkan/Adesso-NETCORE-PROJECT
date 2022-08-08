using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.MoneyPoint;

public class GetMoneyPointByIdQuerie : IRequest<MoneyPointDto>
{
    public int Id { get; set; }

    public GetMoneyPointByIdQuerie(int id)
    {
        Id = id;
    }
}