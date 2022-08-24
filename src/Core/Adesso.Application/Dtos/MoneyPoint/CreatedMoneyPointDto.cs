
namespace Adesso.Application.Dtos.MoneyPoint;

public class CreatedMoneyPointDto : IDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int Point { get; set; }
}
