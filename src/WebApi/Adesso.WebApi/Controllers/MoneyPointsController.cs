using Adesso.Application.Features.Commands.MoneyPoint.Delete;
using Adesso.Application.Features.Commands.MoneyPoint.Update;
using Adesso.Application.Features.Commands.MoneyPoint.Create;
using Adesso.Application.Features.Queries.MoneyPoint;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Adesso.Application.Utilities.Results;
using Adesso.Application.Dtos.MoneyPoint;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoneyPointsController : BaseController
{

    private readonly IMediator _mediator;

    public MoneyPointsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMoneyPoints()
    {
        var result = await _mediator.Send(new GetAllMoneyPointsQuerie());
        return Ok(new SuccessDataResult<List<MoneyPointDto>>(result));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMoneyPointById(int id)
    {
        var result = await _mediator.Send(new GetMoneyPointByIdQuerie(id));
        return Ok(new SuccessDataResult<MoneyPointDto>(result));
    }



    [HttpPost]
    public async Task<IActionResult> CreateMoneyPoint([FromBody] CreateMoneyPointCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<CreateMoneyPointCommand>(command, result));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMoneyPoint(int id, [FromBody] UpdateMoneyPointCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<UpdateMoneyPointCommand>(command, result));

    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteMoneyPointCommand(id);
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<DeleteMoneyPointCommand>(command, result));
    }
}