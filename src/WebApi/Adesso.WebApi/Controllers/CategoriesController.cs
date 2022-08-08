using Adesso.Application.Dtos.Category;
using Adesso.Application.Features.Commands.Category.Create;
using Adesso.Application.Features.Commands.Category.Delete;
using Adesso.Application.Features.Commands.Category.Update;
using Adesso.Application.Features.Queries.Category;
using Adesso.Application.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController
{

    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCateories()
    {
        var result = await _mediator.Send(new GetAllCategoriesQuerie());
        return Ok(new SuccessDataResult<List<CategoryDto>>(result));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuerie(id));
        return Ok(new SuccessDataResult<CategoryDto>(result));
    }



    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<CreateCategoryCommand>(command, result));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<UpdateCategoryCommand>(command, result));

    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteCategoryCommand(id);
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<DeleteCategoryCommand>(command, result));
    }
}
