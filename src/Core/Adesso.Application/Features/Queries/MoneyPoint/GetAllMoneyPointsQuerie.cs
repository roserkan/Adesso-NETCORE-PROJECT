﻿using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.MoneyPoint;

public class GetAllMoneyPointsQuerie : IRequest<IDataResult<List<MoneyPointDto>>>
{
}