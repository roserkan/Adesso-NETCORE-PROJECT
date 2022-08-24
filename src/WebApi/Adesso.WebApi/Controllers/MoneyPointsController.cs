using Microsoft.AspNetCore.Mvc;
using Adesso.Application.Utilities.Results;
using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Features.MoneyPoint.Queries;
using Adesso.Application.Features.MoneyPoint.Commands.Create;
using Adesso.Application.Features.MoneyPoint.Commands.Update;
using Adesso.Application.Features.MoneyPoint.Commands.Delete;
using Adesso.Application.Constants;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoneyPointsController : BaseController
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Mediator.Send(new GetAllMoneyPointsQuerie());
        var result = new SuccessDataResult<List<MoneyPointDto>>(data);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Mediator.Send(new GetMoneyPointByIdQuerie(id));
        var result = new SuccessDataResult<MoneyPointDto>(data);
        return Ok(result);
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMoneyPointCommand command)
    {
        await Mediator.Send(command); // return type: CreatedMoneyPointDto
        var result = new SuccessResult(Messages.MoneyPointCreated);
        return Created("", result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMoneyPointCommand command)
    {
        command.Id = id;
        await Mediator.Send(command); // return type: UpdatedMoneyPointDto
        var result = new SuccessResult(Messages.MoneyPointUpdated);
        return Ok(result);
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteMoneyPointCommand(id);
        await Mediator.Send(command); // return type: DeletedMoneyPointDto
        var result = new SuccessResult(Messages.MoneyPointDeleted);
        return Ok(result);
    }
}