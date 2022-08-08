using Adesso.Application.Constants;
using Adesso.Application.Dtos.Order;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using Adesso.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.Order;

public class GetOrderByIdQuerieHandler : IRequestHandler<GetOrderByIdQuerie, OrderDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetOrderByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.Order>().GetByIdAsync(request.Id);

        var result = _mapper.Map<OrderDto>(category);

        if (result is null)
            throw new DatabaseValidationException(Messages.OrderNotFound);

        return result;
    }
}