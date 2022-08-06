using Adesso.Application.Dtos.Order;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.Order;

public class GetAllOrdersQuerieHandler : IRequestHandler<GetAllOrdersQuerie, IDataResult<List<OrderDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllOrdersQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<OrderDto>>> Handle(GetAllOrdersQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.GetRepository<Domain.Models.Order>().GetAll();

        var result = _mapper.Map<List<OrderDto>>(categories);

        return new SuccessDataResult<List<OrderDto>>(result);
    }
}