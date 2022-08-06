using Adesso.Application.Constants;
using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.MoneyPoint;

public class GetMoneyPointByIdQuerieHandler : IRequestHandler<GetMoneyPointByIdQuerie, IDataResult<MoneyPointDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMoneyPointByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<MoneyPointDto>> Handle(GetMoneyPointByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.MoneyPoint>().GetByIdAsync(request.Id);

        var result = _mapper.Map<MoneyPointDto>(category);

        if (result is null)
            return new ErrorDataResult<MoneyPointDto>(Messages.MoneyPointNotFound);

        return new SuccessDataResult<MoneyPointDto>(result);
    }
}