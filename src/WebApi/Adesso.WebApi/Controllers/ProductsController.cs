using Adesso.Application.Features.Commands.Product.Create;
using Adesso.Application.Features.Commands.Product.Delete;
using Adesso.Application.Features.Commands.Product.Update;
using Adesso.Application.Features.Queries.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{

    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var result = await _mediator.Send(new GetAllProductsQuerie());
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var result = await _mediator.Send(new GetProductByIdQuerie(id));
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }



    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);

        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var command = new DeleteProductCommand(id);
        var result = await _mediator.Send(command);

        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
