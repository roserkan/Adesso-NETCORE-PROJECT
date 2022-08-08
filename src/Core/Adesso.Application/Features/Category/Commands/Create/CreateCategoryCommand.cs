using MediatR;


namespace Adesso.Application.Features.Category.Commands.Create;

public class CreateCategoryCommand : IRequest<string>
{
    public string Name { get; set; }

    public CreateCategoryCommand(string name)
    {
        Name = name;
    }
}
