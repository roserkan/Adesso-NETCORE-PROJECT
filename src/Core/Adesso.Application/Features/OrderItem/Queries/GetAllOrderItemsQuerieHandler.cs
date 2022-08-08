using Adesso.Application.Dtos.OrderItem;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.OrderItem.Queries;

public class GetAllOrderItemsQuerieHandler : IRequestHandler<GetAllOrderItemsQuerie, List<OrderItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.OrderItem> _orderItemRepository;


    public GetAllOrderItemsQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _orderItemRepository = _unitOfWork.GetRepository<Domain.Models.OrderItem>();

    }

    public async Task<List<OrderItemDto>> Handle(GetAllOrderItemsQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _orderItemRepository.GetAll();

        var result = _mapper.Map<List<OrderItemDto>>(categories);

        return result;
    }
}