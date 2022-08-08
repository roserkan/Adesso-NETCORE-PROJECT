using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.MoneyPoint;

public class GetAllMoneyPointsQuerieHandler : IRequestHandler<GetAllMoneyPointsQuerie, List<MoneyPointDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.MoneyPoint> _moneyPointRepository;


    public GetAllMoneyPointsQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _moneyPointRepository = unitOfWork.GetRepository<Domain.Models.MoneyPoint>();

    }

    public async Task<List<MoneyPointDto>> Handle(GetAllMoneyPointsQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _moneyPointRepository.GetAll();

        var result = _mapper.Map<List<MoneyPointDto>>(categories);

        return result;
    }
}