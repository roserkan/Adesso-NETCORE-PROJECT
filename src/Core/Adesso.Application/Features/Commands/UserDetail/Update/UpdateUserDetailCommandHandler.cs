using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.UserDetail.Update;

public class UpdateUserDetailCommandHandler : IRequestHandler<UpdateUserDetailCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(UpdateUserDetailCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(
                await CheckUserDetailExist(request.Id),
                await CheckUserExist(request.UserId)
             );

        var user = _mapper.Map<Domain.Models.UserDetail>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.UserDetail>().UpdateAsync(user);

        return Messages.UserDetailUpdated;
    }



    private async Task<IResult> CheckUserExist(int userId)
    {
        var user = await _unitOfWork.GetRepository<Domain.Models.User>()
            .GetSingleAsync(u => u.Id == userId);
        

        if (user is null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }

        return new SuccessResult();
    }

    private async Task<IResult> CheckUserDetailExist(int id)
    {
        var userDetail = await _unitOfWork.GetRepository<Domain.Models.UserDetail>().GetByIdAsync(id);
        if (userDetail is null)
        {
            return new ErrorResult(Messages.UserDetailNotFound);
        }
        return new SuccessResult();
    }

}
