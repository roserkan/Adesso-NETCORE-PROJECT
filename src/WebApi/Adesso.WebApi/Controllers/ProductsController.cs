using Adesso.Application.Dtos.Product;
using Adesso.Application.Features.Product.Commands.Create;
using Adesso.Application.Features.Product.Commands.Delete;
using Adesso.Application.Features.Product.Commands.Update;
using Adesso.Application.Features.Product.Queries;
using Adesso.Application.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : BaseController
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
        return Ok(new SuccessDataResult<List<ProductDto>>(result));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var result = await _mediator.Send(new GetProductByIdQuerie(id));
        return Ok(new SuccessDataResult<ProductDto>(result));
    }



    [HttpPost]
    public  async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<CreateProductCommand>(command, result));
    }
        
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<UpdateProductCommand>(command, result));

    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteProductCommand(id);
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<DeleteProductCommand>(command, result));
    }
}
