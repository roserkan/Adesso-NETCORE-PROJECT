using Adesso.Application.Dtos.Product;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.Product;

public class GetAllProductsQuerie : IRequest<List<ProductDto>>
{
}
