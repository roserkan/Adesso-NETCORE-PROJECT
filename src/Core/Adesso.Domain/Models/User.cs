namespace Adesso.Domain.Models;

public class User : BaseEntity
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<UserRole> UserRole { get; set; }

}

