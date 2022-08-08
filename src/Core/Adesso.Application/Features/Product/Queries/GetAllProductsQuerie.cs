using Adesso.Application.Dtos.Product;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Product.Queries;

public class GetAllProductsQuerie : IRequest<List<ProductDto>>
{
}
