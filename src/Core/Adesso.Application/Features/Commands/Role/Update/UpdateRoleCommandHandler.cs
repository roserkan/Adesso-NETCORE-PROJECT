﻿using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.Role.Update;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, string>
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

    public async Task<string> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(
                await CheckRoleExist(request.Id),
                await CheckRoleNameExist(request.RoleName)
            );

        var product = _mapper.Map<Domain.Models.Role>(request);

        var rows = await _roleRepository.UpdateAsync(product);

        return Messages.RoleUpdated;
    }


   


    private async Task<IResult> CheckRoleNameExist(string roleName)
    {
        var role = await _roleRepository
            .GetSingleAsync(r => r.RoleName == roleName);

        if (role is not null)
        {
            return new ErrorResult(Messages.RoleNameAlreadyExist);
        }


        return new SuccessResult();
    }

    private async Task<IResult> CheckRoleExist(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role is null)
        {
            return new ErrorResult(Messages.RoleNotFound);
        }
        return new SuccessResult();
    }



}
