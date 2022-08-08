using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using Adesso.Application.Utilities.Security;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adesso.Application.Features.Commands.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private IGenericRepository<Domain.Models.User> _userRepository;


    public CreateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userRepository = _unitOfWork.GetRepository<Domain.Models.User>();
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(await CheckEmailAddressExist(request.EmailAddress));

        var user = _mapper.Map<Domain.Models.User>(request);
        user.Password = PasswordEncryptor.Encrypt(user.Password);
        var rows = await _userRepository.AddAsync(user);

        return Messages.UserCreated;
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