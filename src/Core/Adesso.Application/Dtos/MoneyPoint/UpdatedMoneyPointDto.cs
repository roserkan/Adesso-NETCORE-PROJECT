
namespace Adesso.Application.Dtos.MoneyPoint;

public class UpdatedMoneyPointDto : IDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int Point { get; set; }
}
