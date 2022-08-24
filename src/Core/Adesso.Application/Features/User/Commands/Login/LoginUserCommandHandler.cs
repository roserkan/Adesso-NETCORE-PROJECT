using Adesso.Application.Dtos.User;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Security;
using Adesso.Application.Utilities.Security.Jwt;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;

namespace Adesso.Application.Features.User.Commands.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private IGenericRepository<Domain.Models.User> _userRepository;


    public LoginUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration;
        _userRepository = _unitOfWork.GetRepository<Domain.Models.User>();

    }

    public async Task<LoginUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await _userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);
        if (dbUser == null)
            throw new BusinessException(Messages.UserNotFound);

        var encryptedPassword = PasswordEncryptor.Encrypt(request.Password);
        if (dbUser.Password != encryptedPassword)
            throw new BusinessException(Messages.PasswordWrongError);



        var result = _mapper.Map<LoginUserDto>(dbUser);

        var query =  _unitOfWork.GetRepository<Domain.Models.User>().AsQueryable();
        var user = await query.Where(i => i.Id == dbUser.Id).Include(i => i.UserRole).SingleOrDefaultAsync();
        var roleIds = user.UserRole.Select(i => i.RoleId).ToList();
        var roleNames = new List<string>();
        foreach (var roleId in roleIds)
        {
            var role = await _unitOfWork.GetRepository<Domain.Models.Role>().GetByIdAsync(roleId);
            roleNames.Add(role.RoleName);
        }
        
        var claims = CreateClaimHelper.CreateClaim(user, roleNames);


        result.Token = GenerateTokenHelper.GenerateToken(claims, _configuration);

        return result;


    }
}
