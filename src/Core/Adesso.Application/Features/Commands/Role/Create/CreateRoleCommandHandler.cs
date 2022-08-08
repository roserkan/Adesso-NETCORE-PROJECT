using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.Role.Create;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(
                await CheckRoleNameExist(request.RoleName)

            );
      

        var moneyPoint = _mapper.Map<Domain.Models.Role>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.Role>().AddAsync(moneyPoint);

        return Messages.RoleCreated;
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
