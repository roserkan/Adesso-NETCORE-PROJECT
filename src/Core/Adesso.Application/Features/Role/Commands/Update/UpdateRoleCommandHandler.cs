using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.Role;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Role.Commands.Update;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, UpdatedRoleDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.Role> _roleRepository;


    public UpdateRoleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _roleRepository = _unitOfWork.GetRepository<Domain.Models.Role>();

    }

    public async Task<UpdatedRoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {

        await this.CheckRoleExist(request.Id);
        await this.CheckRoleNameExist(request.RoleName);

        var role = _mapper.Map<Domain.Models.Role>(request);

        await _roleRepository.UpdateAsync(role);
        var updatedRole = _mapper.Map<UpdatedRoleDto>(role);
        return updatedRole;
    }


   


    private async Task CheckRoleNameExist(string roleName)
    {
        var role = await _roleRepository
            .GetSingleAsync(r => r.RoleName == roleName);

        if (role is not null) throw new BusinessException(Messages.RoleNameAlreadyExist);
    }

    private async Task CheckRoleExist(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role is null) throw new BusinessException(Messages.RoleNotFound);
    }



}
