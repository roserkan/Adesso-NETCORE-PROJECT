using Adesso.Application.Dtos.Order;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Order.Queries;

public class GetAllOrdersQuerieHandler : IRequestHandler<GetAllOrdersQuerie, List<OrderDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.Order> _orderRepository;


    public GetAllOrdersQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _orderRepository = _unitOfWork.GetRepository<Domain.Models.Order>();
    }

    public async Task<List<OrderDto>> Handle(GetAllOrdersQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _orderRepository.GetAll();

        var result = _mapper.Map<List<OrderDto>>(categories);

        return result;
    }
}