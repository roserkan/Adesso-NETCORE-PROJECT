using Adesso.Application.Constants;
using Adesso.Application.Dtos.Product;
using Adesso.Application.Features.Product.Commands.Create;
using Adesso.Application.Features.Product.Commands.Delete;
using Adesso.Application.Features.Product.Commands.Update;
using Adesso.Application.Features.Product.Queries;
using Adesso.Application.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : BaseController
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Mediator.Send(new GetAllProductsQuerie());
        var result = new SuccessDataResult<List<ProductDto>>(data);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Mediator.Send(new GetProductByIdQuerie(id));
        var result = new SuccessDataResult<ProductDto>(data);
        return Ok(result);
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        await Mediator.Send(command); // return type: CreatedProductDto
        var result = new SuccessResult(Messages.ProductCreated);
        return Created("", result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
    {
        command.Id = id;
        await Mediator.Send(command); // return type: UpdatedProductDto
        var result = new SuccessResult(Messages.ProductUpdated);
        return Ok(result);
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteProductCommand(id);
        await Mediator.Send(command); // return type: DeletedProductDto
        var result = new SuccessResult(Messages.ProductDeleted);
        return Ok(result);
    }
}
