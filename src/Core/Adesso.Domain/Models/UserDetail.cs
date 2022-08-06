

using Adesso.Domain.Enums;

namespace Adesso.Domain.Models;

public class UserDetail : BaseEntity
{
    public User User { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Genders Gender { get; set; }
}
