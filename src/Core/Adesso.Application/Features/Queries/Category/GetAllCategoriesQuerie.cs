using Adesso.Application.Dtos.Category;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.Category;

public class GetAllCategoriesQuerie : IRequest<IDataResult<List<CategoryDto>>>
{
}
