using Adesso.Application.Constants;
using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using Adesso.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.MoneyPoint;

public class GetMoneyPointByIdQuerieHandler : IRequestHandler<GetMoneyPointByIdQuerie, MoneyPointDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMoneyPointByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MoneyPointDto> Handle(GetMoneyPointByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.MoneyPoint>().GetByIdAsync(request.Id);

        var result = _mapper.Map<MoneyPointDto>(category);

        if (result is null)
            throw new DatabaseValidationException(Messages.MoneyPointNotFound);


        return result;
    }
}