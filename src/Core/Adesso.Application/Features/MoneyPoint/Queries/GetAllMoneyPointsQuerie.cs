using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.MoneyPoint.Queries;

public class GetAllMoneyPointsQuerie : IRequest<List<MoneyPointDto>>
{
}
