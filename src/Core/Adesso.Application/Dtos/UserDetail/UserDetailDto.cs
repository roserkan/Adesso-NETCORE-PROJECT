

using Adesso.Domain.Enums;

namespace Adesso.Application.Dtos.UserDetail;

public class UserDetailDto : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Genders Gender { get; set; }
    public DateTime CreatedTime { get; set; }
}

