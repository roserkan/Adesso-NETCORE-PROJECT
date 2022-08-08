using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.UserDetail.Commands.Create;

public class CreateUserDetailCommandHandler : IRequestHandler<CreateUserDetailCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private IGenericRepository<Domain.Models.UserDetail> _userDetailRepository;
    private IGenericRepository<Domain.Models.User> _userRepository;


    public CreateUserDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userDetailRepository = _unitOfWork.GetRepository<Domain.Models.UserDetail>();
        _userRepository = _unitOfWork.GetRepository<Domain.Models.User>();

    }

    public async Task<string> Handle(CreateUserDetailCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(
                await CheckUserExist(request.UserId)
            );
      

        var user = _mapper.Map<Domain.Models.UserDetail>(request);
        var rows = await _userDetailRepository.AddAsync(user);

        return Messages.UserDetailCreated;
    }

    private async Task<IResult> CheckUserExist(int userId)
    {
        var user = await _userRepository
            .GetSingleAsync(u => u.Id == userId);
        var userDetail = await _userDetailRepository
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