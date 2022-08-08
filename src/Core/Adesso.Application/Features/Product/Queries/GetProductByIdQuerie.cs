using Adesso.Application.Dtos.Product;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Product.Queries;

public class GetProductByIdQuerie : IRequest<ProductDto>
{
    public int Id { get; set; }

    public GetProductByIdQuerie(int id)
    {
        Id = id;
    }
}