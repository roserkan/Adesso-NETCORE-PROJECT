using Adesso.Application.Features.Commands.Category.Delete;
using Adesso.Application.Features.Commands.Category.Update;
using Adesso.Application.Features.Commands.MoneyPoint.Create;
using Adesso.Application.Features.Commands.MoneyPoint.Delete;
using Adesso.Application.Features.Commands.MoneyPoint.Update;
using Adesso.Application.Features.Queries.MoneyPoint;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMoneyPointById(int id)
    {
        var result = await _mediator.Send(new GetMoneyPointByIdQuerie(id));
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }



    [HttpPost]
    public async Task<IActionResult> CreateMoneyPoint([FromBody] CreateMoneyPointCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateMoneyPoint(int id, [FromBody] UpdateMoneyPointCommand command)
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
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteMoneyPointCommand(id);
        var result = await _mediator.Send(command);

        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
