using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.UserDetail;

public class GetAllUserDetailsQuerieHandler : IRequestHandler<GetAllUserDetailsQuerie, List<UserDetailDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllUserDetailsQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<UserDetailDto>> Handle(GetAllUserDetailsQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.GetRepository<Domain.Models.UserDetail>().GetAll();

        var result = _mapper.Map<List<UserDetailDto>>(categories);

        return result;
    }
}