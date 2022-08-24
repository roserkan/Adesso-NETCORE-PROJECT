

namespace Adesso.Application.Dtos.User;

public class DeletedUserDto : IDto
{
    public int Id { get; set; }
    public string EmailAddress { get; set; }
}
