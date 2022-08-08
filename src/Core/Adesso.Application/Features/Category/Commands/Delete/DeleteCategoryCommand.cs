using MediatR;


namespace Adesso.Application.Features.Category.Commands.Delete;

public class DeleteCategoryCommand: IRequest<string>
{
    public DeleteCategoryCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
