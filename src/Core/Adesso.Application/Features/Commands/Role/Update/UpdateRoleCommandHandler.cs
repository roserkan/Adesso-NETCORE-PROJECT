﻿using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.Role.Update;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, IDataResult<UpdateRoleCommand>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IDataResult<UpdateRoleCommand>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(
                await CheckRoleExist(request.Id),
                await CheckRoleNameExist(request.RoleName)
            );
        if (result != null)
        {
            return new ErrorDataResult<UpdateRoleCommand>(result.Message);
        }

        var product = _mapper.Map<Domain.Models.Role>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.Role>().UpdateAsync(product);

        return new SuccessDataResult<UpdateRoleCommand>(request, Messages.RoleUpdated);
    }


    private async Task<IResult> CheckRoleExist(int id)
    {
        var moneyPoint = await _unitOfWork.GetRepository<Domain.Models.Role>().GetByIdAsync(id);
        if (moneyPoint is null)
        {
            return new ErrorResult(Messages.RoleNotFound);
        }
        return new SuccessResult();
    }


    private async Task<IResult> CheckRoleNameExist(string roleName)
    {
        var role = await _unitOfWork.GetRepository<Domain.Models.Role>()
            .GetSingleAsync(r => r.RoleName == roleName);

        if (role is not null)
        {
            return new ErrorResult(Messages.RoleNameAlreadyExist);
        }


        return new SuccessResult();
    }



}