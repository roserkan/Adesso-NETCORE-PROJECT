using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.Role;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Role.Queries;

public class GetRoleByIdQuerieHandler : IRequestHandler<GetRoleByIdQuerie, RoleDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.Role> _roleRepository;


    public GetRoleByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _roleRepository = _unitOfWork.GetRepository<Domain.Models.Role>();
    }

    public async Task<RoleDto> Handle(GetRoleByIdQuerie request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id);

        var result = _mapper.Map<RoleDto>(role);

        if (result is null)
            throw new BusinessException(Messages.RoleNotFound);

        return result;
    }
}