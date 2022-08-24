using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.Role;
using Adesso.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Role.Commands.Delete;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, DeletedRoleDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.Role> _roleRepository;


    public DeleteRoleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _roleRepository = _unitOfWork.GetRepository<Domain.Models.Role>();

    }

    public async Task<DeletedRoleDto> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {

        await this.CheckRoleExsist(request.Id);

        var role = _mapper.Map<Domain.Models.Role>(request);
        await _roleRepository.DeleteAsync(role);
        var deletedRole = _mapper.Map<DeletedRoleDto>(role);
        return deletedRole;
    }

    private async Task CheckRoleExsist(int id)
    {
        var product = await _roleRepository.GetByIdAsync(id);
        if (product is null) throw new BusinessException(Messages.RoleNotFound);
    }
}
