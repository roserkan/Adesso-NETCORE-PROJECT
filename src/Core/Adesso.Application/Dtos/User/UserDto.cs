

namespace Adesso.Application.Dtos.User;

public class UserDto: IDto
{
    public int Id { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public DateTime CreatedDate { get; set; }
}
