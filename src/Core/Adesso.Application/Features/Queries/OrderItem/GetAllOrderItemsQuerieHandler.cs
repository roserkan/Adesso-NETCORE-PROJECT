using Adesso.Application.Dtos.OrderItem;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.OrderItem;

public class GetAllOrderItemsQuerieHandler : IRequestHandler<GetAllOrderItemsQuerie, List<OrderItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllOrderItemsQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<OrderItemDto>> Handle(GetAllOrderItemsQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.GetRepository<Domain.Models.OrderItem>().GetAll();

        var result = _mapper.Map<List<OrderItemDto>>(categories);

        return result;
    }
}