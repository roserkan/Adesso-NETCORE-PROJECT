using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.UserDetail.Commands.Update;

public class UpdateUserDetailCommandHandler : IRequestHandler<UpdateUserDetailCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.UserDetail> _userDetailRepository;
    private IGenericRepository<Domain.Models.User> _userRepository;

    public UpdateUserDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userDetailRepository = _unitOfWork.GetRepository<Domain.Models.UserDetail>();
        _userRepository = _unitOfWork.GetRepository<Domain.Models.User>();
    }

    public async Task<string> Handle(UpdateUserDetailCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(
                await CheckUserDetailExist(request.Id),
                await CheckUserExist(request.UserId)
             );

        var user = _mapper.Map<Domain.Models.UserDetail>(request);

        var rows = await _userDetailRepository.UpdateAsync(user);

        return Messages.UserDetailUpdated;
    }



    private async Task<IResult> CheckUserExist(int userId)
    {
        var user = await _userRepository
            .GetSingleAsync(u => u.Id == userId);
        

        if (user is null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }

        return new SuccessResult();
    }

    private async Task<IResult> CheckUserDetailExist(int id)
    {
        var userDetail = await _userDetailRepository.GetByIdAsync(id);
        if (userDetail is null)
        {
            return new ErrorResult(Messages.UserDetailNotFound);
        }
        return new SuccessResult();
    }

}
