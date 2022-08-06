using Adesso.Application.Dtos.Product;
using Adesso.Application.Utilities.Results;
using MediatR;

namespace Adesso.Application.Features.Queries.Product;

public class GetProductByIdQuerie : IRequest<IDataResult<ProductDto>>
{
    public int Id { get; set; }

    public GetProductByIdQuerie(int id)
    {
        Id = id;
    }
}