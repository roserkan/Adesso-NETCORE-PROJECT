using Adesso.Application.Features.Commands.Role.Create;
using Adesso.Application.Features.Commands.Role.Delete;
using Adesso.Application.Features.Commands.Role.Update;
using Adesso.Application.Features.Queries.Role;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : BaseController
{

    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoless()
    {
        var result = await _mediator.Send(new GetAllRolesQuerie());
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRolesById(int id)
    {
        var result = await _mediator.Send(new GetRoleByIdQuerie(id));
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }



    [HttpPost]
    public async Task<IActionResult> CreateRoles([FromBody] CreateRoleCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRoles(int id, [FromBody] UpdateRoleCommand command)
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
        var command = new DeleteRoleCommand(id);
        var result = await _mediator.Send(command);

        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
