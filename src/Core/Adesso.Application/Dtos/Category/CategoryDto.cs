namespace Adesso.Application.Dtos.Category;

public class CategoryDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MoneyPointId { get; set; }
    public DateTime CreatedDate { get; set; }
}
