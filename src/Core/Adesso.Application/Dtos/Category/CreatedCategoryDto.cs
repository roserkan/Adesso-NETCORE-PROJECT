using Adesso.Application.Dtos.MoneyPoint;

namespace Adesso.Application.Dtos.Category;

public class CreatedCategoryDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

