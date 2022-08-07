using Adesso.Domain.Enums;

namespace Adesso.Domain.Models;

public class Role : BaseEntity
{
    public string RoleName { get; set; }
    public virtual ICollection<UserRole> UserRole { get; set; }

}
    
