using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.Role.Delete;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, IDataResult<DeleteRoleCommand>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IDataResult<DeleteRoleCommand>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(
            await CheckRoleExsist(request.Id)
            );
        if (result != null)
        {
            return new ErrorDataResult<DeleteRoleCommand>(result.Message);
        }

        var product = _mapper.Map<Domain.Models.Role>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.Role>().DeleteAsync(product);

        return new SuccessDataResult<DeleteRoleCommand>(request, Messages.RoleDeleted);
    }

    private async Task<IResult> CheckRoleExsist(int id)
    {
        var product = await _unitOfWork.GetRepository<Domain.Models.Role>().GetByIdAsync(id);
        if (product is null)
        {
            return new ErrorResult(Messages.RoleNotFound);
        }
        return new SuccessResult();
    }
}
