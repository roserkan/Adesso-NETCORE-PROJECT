using Adesso.Application.Dtos.Role;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Role.Queries;

public class GetAllRolesQuerieHandler : IRequestHandler<GetAllRolesQuerie, List<RoleDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.Role> _roleRepository;


    public GetAllRolesQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _roleRepository = _unitOfWork.GetRepository<Domain.Models.Role>();
    }

    public async Task<List<RoleDto>> Handle(GetAllRolesQuerie request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAll();

        var result = _mapper.Map<List<RoleDto>>(roles);

        return result;
    }
}