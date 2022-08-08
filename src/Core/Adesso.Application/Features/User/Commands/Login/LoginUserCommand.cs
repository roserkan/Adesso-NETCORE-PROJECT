using Adesso.Application.Dtos.User;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.User.Commands.Login;

public class LoginUserCommand: IRequest<LoginUserDto>
{
    public LoginUserCommand(string emailAddress, string password)
    {
        EmailAddress = emailAddress;
        Password = password;
    }

    public string EmailAddress { get; set; }
    public string Password { get; set; }
}
