using Adesso.Application.Dtos.Category;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.Category;

public class GetCategoryByIdQuerie : IRequest<IDataResult<CategoryDto>>
{
    public int Id { get; set; }

    public GetCategoryByIdQuerie(int id)
    {
        Id = id;
    }
}