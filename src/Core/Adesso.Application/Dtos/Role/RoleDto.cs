
namespace Adesso.Application.Dtos.Role;

public class RoleDto: IDto
{
    public int Id { get; set; }
    public string RoleName { get; set; }
    public DateTime CreatedDate { get; set; }

}
