

using Adesso.Domain.Enums;

namespace Adesso.Application.Dtos.UserDetail;

public class DeletedUserDetailDto : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public Genders Gender { get; set; }
}
