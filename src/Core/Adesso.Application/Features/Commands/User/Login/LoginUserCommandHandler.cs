using Adesso.Application.Constants;
using Adesso.Application.Dtos.User;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using Adesso.Application.Utilities.Security;
using Adesso.Application.Utilities.Security.Jwt;
using Adesso.Domain.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Adesso.Application.Features.Commands.User.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, IDataResult<LoginUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public LoginUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<IDataResult<LoginUserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await _unitOfWork.GetRepository<Domain.Models.User>().GetSingleAsync(i => i.EmailAddress == request.EmailAddress);
        if (dbUser == null)
            throw new DatabaseValidationException(Messages.UserNotFound);

        var encryptedPassword = PasswordEncryptor.Encrypt(request.Password);
        if (dbUser.Password != encryptedPassword)
            throw new DatabaseValidationException(Messages.PasswordWrongError);



        var result = _mapper.Map<LoginUserDto>(dbUser);

        var claims = CreateClaimHelper.CreateClaim(dbUser);


        result.Token = GenerateTokenHelper.GenerateToken(claims, _configuration);

        return new SuccessDataResult<LoginUserDto>(result);


    }
}
