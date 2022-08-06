using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.User.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IDataResult<DeleteUserCommand>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IDataResult<DeleteUserCommand>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(await CheckUserExist(request.Id));
        if (result != null)
        {
            return new ErrorDataResult<DeleteUserCommand>(result.Message);
        }

        var category = _mapper.Map<Domain.Models.User>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.User>().DeleteAsync(category);

        return new SuccessDataResult<DeleteUserCommand>(request, Messages.UserDeleted);
    }

    private async Task<IResult> CheckUserExist(int id)
    {
        var user = await _unitOfWork.GetRepository<Domain.Models.User>().GetByIdAsync(id);
        if (user is null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }
        return new SuccessResult();
    }




}
