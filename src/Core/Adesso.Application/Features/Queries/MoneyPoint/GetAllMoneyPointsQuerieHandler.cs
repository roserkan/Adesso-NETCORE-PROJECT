using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.MoneyPoint;

public class GetAllMoneyPointsQuerieHandler : IRequestHandler<GetAllMoneyPointsQuerie, IDataResult<List<MoneyPointDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllMoneyPointsQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<MoneyPointDto>>> Handle(GetAllMoneyPointsQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.GetRepository<Domain.Models.MoneyPoint>().GetAll();

        var result = _mapper.Map<List<MoneyPointDto>>(categories);

        return new SuccessDataResult<List<MoneyPointDto>>(result);
    }
}