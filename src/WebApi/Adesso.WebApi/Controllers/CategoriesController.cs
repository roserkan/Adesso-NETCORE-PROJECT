using Adesso.Application.Constants;
using Adesso.Application.Dtos.Category;
using Adesso.Application.Features.Category.Commands.Create;
using Adesso.Application.Features.Category.Commands.Delete;
using Adesso.Application.Features.Category.Commands.Update;
using Adesso.Application.Features.Category.Queries;
using Adesso.Application.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController
{


    [HttpGet]
    //[Authorize("AllowedAdmin")]
    public async Task<IActionResult> GetAll()
    {
        var data = await Mediator.Send(new GetAllCategoriesQuerie());
        var result = new SuccessDataResult<List<CategoryDto>>(data);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Mediator.Send(new GetCategoryByIdQuerie(id));
        var result = new SuccessDataResult<CategoryDto>(data);
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        await Mediator.Send(command); // return type: CreatedCategoryDto
        var result = new SuccessResult(Messages.CategoryCreated);
        // data, succces, message
        return Created("", result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommand command)
    {
        command.Id = id;
        await Mediator.Send(command); // return type: UpdatedCategoryDto
        var result = new SuccessResult(Messages.CategoryUpdated);
        return Ok(result);
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteCategoryCommand(id);
        await Mediator.Send(command); // return type: DeletedCategoryDto
        var result = new SuccessResult(Messages.CategoryDeleted);
        return Ok(result);
    }
}
