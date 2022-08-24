using Adesso.Application.Constants;
using Adesso.Application.Dtos.User;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using Adesso.Application.Utilities.Security;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.User.Commands.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.User> _userRepository;


    public UpdateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userRepository = _unitOfWork.GetRepository<Domain.Models.User>();

    }

    public async Task<UpdatedUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {

        await this.CheckUserExsist(request.Id);
        await this.CheckEmailAddressExist(request.EmailAddress);

        var user = _mapper.Map<Domain.Models.User>(request);
        user.Password = PasswordEncryptor.Encrypt(user.Password);

        await _userRepository.UpdateAsync(user);
        var updatedUser = _mapper.Map<UpdatedUserDto>(user);
        return updatedUser;
    }



    private async Task<IResult> CheckUserExsist(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }
        return new SuccessResult();
    }

    private async Task<IResult> CheckEmailAddressExist(string emailAddress)
    {
        var user = await _userRepository.GetSingleAsync(u => u.EmailAddress == emailAddress);
        if (user is not null)
        {
            return new ErrorResult(Messages.UserEmailAddressNotAvailable);
        }
        return new SuccessResult();
    }



}
