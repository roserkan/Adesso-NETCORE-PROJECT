using Adesso.Application.Dtos.Role;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.Role;

public class GetAllRolesQuerieHandler : IRequestHandler<GetAllRolesQuerie, List<RoleDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllRolesQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<RoleDto>> Handle(GetAllRolesQuerie request, CancellationToken cancellationToken)
    {
        var roles = await _unitOfWork.GetRepository<Domain.Models.Role>().GetAll();

        var result = _mapper.Map<List<RoleDto>>(roles);

        return result;
    }
}