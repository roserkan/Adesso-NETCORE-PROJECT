
namespace Adesso.Application.Dtos.MoneyPoint;

public class MoneyPointDto : IDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int Point { get; set; }
    public DateTime CreatedDate { get; set; }
}
