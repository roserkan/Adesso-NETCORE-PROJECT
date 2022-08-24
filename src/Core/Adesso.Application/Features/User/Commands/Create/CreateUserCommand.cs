using Adesso.Application.Dtos.User;
using MediatR;

namespace Adesso.Application.Features.User.Commands.Create;

public class CreateUserCommand: IRequest<CreatedUserDto>
{
    public CreateUserCommand(string emailAddress, string password)
    {
        EmailAddress = emailAddress;
        Password = password;
    }

    public string EmailAddress { get; set; }
    public string Password { get; set; }
}
