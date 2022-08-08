using Adesso.Application.Constants;
using Adesso.Application.Dtos.OrderItem;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using Adesso.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.OrderItem.Queries;

public class GetOrderItemByIdQuerieHandler : IRequestHandler<GetOrderItemByIdQuerie, OrderItemDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.OrderItem> _orderItemRepository;


    public GetOrderItemByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _orderItemRepository = _unitOfWork.GetRepository<Domain.Models.OrderItem>();

    }

    public async Task<OrderItemDto> Handle(GetOrderItemByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _orderItemRepository.GetByIdAsync(request.Id);

        var result = _mapper.Map<OrderItemDto>(category);

        if (result is null)
            throw new DatabaseValidationException(Messages.OrderItemNotFound);

        return result;
    }
}