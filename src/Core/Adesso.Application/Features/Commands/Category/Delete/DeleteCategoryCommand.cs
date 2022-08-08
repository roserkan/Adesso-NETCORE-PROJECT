using MediatR;


namespace Adesso.Application.Features.Commands.Category.Delete;

public class DeleteCategoryCommand: IRequest<string>
{
    public DeleteCategoryCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
