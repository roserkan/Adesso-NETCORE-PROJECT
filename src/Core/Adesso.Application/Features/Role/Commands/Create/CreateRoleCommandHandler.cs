using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.Role;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Role.Commands.Create;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreatedRoleDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.Role> _roleRepository;


    public CreateRoleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _roleRepository = _unitOfWork.GetRepository<Domain.Models.Role>();

    }

    public async Task<CreatedRoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        await this.CheckRoleNameExist(request.RoleName);

      

        var role = _mapper.Map<Domain.Models.Role>(request);
        await _roleRepository.AddAsync(role);
        var createdRole = _mapper.Map<CreatedRoleDto>(role);
        return createdRole;
    }


    private async Task CheckRoleNameExist(string roleName)
    {
        var role = await _roleRepository
            .GetSingleAsync(r => r.RoleName == roleName);

        if (role is not null) throw new BusinessException(Messages.RoleNameAlreadyExist);
    }

   


}
