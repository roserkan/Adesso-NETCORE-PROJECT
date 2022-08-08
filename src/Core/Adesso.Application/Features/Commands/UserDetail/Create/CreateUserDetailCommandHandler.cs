using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.UserDetail.Create;

public class CreateUserDetailCommandHandler : IRequestHandler<CreateUserDetailCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(CreateUserDetailCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(
                await CheckUserExist(request.UserId)
            );
      

        var user = _mapper.Map<Domain.Models.UserDetail>(request);
        var rows = await _unitOfWork.GetRepository<Domain.Models.UserDetail>().AddAsync(user);

        return Messages.UserDetailCreated;
    }

    private async Task<IResult> CheckUserExist(int userId)
    {
        var user = await _unitOfWork.GetRepository<Domain.Models.User>()
            .GetSingleAsync(u => u.Id == userId);
        var userDetail = await _unitOfWork.GetRepository<Domain.Models.UserDetail>()
            .GetSingleAsync(u => u.UserId == userId);

        if (user is null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }

        if (userDetail is not null)
        {
            return new ErrorResult(Messages.UserDetailAlreadyExist);
        }
        return new SuccessResult();
    }







}