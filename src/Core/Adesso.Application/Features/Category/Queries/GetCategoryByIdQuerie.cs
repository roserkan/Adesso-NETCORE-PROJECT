using Adesso.Application.Dtos.Category;
using MediatR;

namespace Adesso.Application.Features.Category.Queries;

public class GetCategoryByIdQuerie : IRequest<CategoryDto>
{
    public int Id { get; set; }

    public GetCategoryByIdQuerie(int id)
    {
        Id = id;
    }
}