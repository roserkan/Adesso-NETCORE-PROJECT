using Adesso.Application.Dtos.Category;
using MediatR;

namespace Adesso.Application.Features.Category.Commands.Update;

public class UpdateCategoryCommand: IRequest<UpdatedCategoryDto>
{
    public UpdateCategoryCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}
