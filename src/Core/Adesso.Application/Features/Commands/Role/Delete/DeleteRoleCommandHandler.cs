using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.Role.Delete;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, string>
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

    public async Task<string> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(
            await CheckRoleExsist(request.Id)
            );

        var product = _mapper.Map<Domain.Models.Role>(request);

        var rows = await _roleRepository.DeleteAsync(product);

        return Messages.RoleDeleted;
    }

    private async Task<IResult> CheckRoleExsist(int id)
    {
        var product = await _roleRepository.GetByIdAsync(id);
        if (product is null)
        {
            return new ErrorResult(Messages.RoleNotFound);
        }
        return new SuccessResult();
    }
}
