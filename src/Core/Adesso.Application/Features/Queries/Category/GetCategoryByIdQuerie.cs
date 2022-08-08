using Adesso.Application.Dtos.Category;
using MediatR;

namespace Adesso.Application.Features.Queries.Category;

public class GetCategoryByIdQuerie : IRequest<CategoryDto>
{
    public int Id { get; set; }

    public GetCategoryByIdQuerie(int id)
    {
        Id = id;
    }
}