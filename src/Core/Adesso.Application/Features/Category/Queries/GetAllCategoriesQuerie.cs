using Adesso.Application.Dtos.Category;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Category.Queries;

public class GetAllCategoriesQuerie : IRequest<List<CategoryDto>>
{
}
