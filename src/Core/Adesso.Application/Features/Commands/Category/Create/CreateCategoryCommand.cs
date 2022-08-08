using Adesso.Application.Utilities.Results;
using MediatR;


namespace Adesso.Application.Features.Commands.Category.Create;

public class CreateCategoryCommand : IRequest<string>
{
    public string Name { get; set; }

    public CreateCategoryCommand(string name)
    {
        Name = name;
    }
}
