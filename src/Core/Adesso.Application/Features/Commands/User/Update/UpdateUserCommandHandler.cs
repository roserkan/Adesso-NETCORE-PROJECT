using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using Adesso.Application.Utilities.Security;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.User.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(
                await CheckUserExsist(request.Id),
                await CheckEmailAddressExist(request.EmailAddress)
             );

        var user = _mapper.Map<Domain.Models.User>(request);
        user.Password = PasswordEncryptor.Encrypt(user.Password);

        var rows = await _unitOfWork.GetRepository<Domain.Models.User>().UpdateAsync(user);

        return Messages.UserUpdated;
    }



    private async Task<IResult> CheckUserExsist(int id)
    {
        var user = await _unitOfWork.GetRepository<Domain.Models.User>().GetByIdAsync(id);
        if (user is null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }
        return new SuccessResult();
    }

    private async Task<IResult> CheckEmailAddressExist(string emailAddress)
    {
        var user = await _unitOfWork.GetRepository<Domain.Models.User>().GetSingleAsync(u => u.EmailAddress == emailAddress);
        if (user is not null)
        {
            return new ErrorResult(Messages.UserEmailAddressNotAvailable);
        }
        return new SuccessResult();
    }



}
