

namespace Adesso.Application.Dtos.User;

public class LoginUserDto: IDto
{
    public int Id { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
}
