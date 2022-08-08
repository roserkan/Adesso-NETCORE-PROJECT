using Adesso.Application.Dtos.Role;
using Adesso.Application.Features.Role.Commands.Create;
using Adesso.Application.Features.Role.Commands.Delete;
using Adesso.Application.Features.Role.Commands.Update;
using Adesso.Application.Features.Role.Queries;
using Adesso.Application.Utilities.Results;
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
    public async Task<IActionResult> GetAllRoles()
    {
        var result = await _mediator.Send(new GetAllRolesQuerie());
        return Ok(new SuccessDataResult<List<RoleDto>>(result));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRoleById(int id)
    {
        var result = await _mediator.Send(new GetRoleByIdQuerie(id));
        return Ok(new SuccessDataResult<RoleDto>(result));
    }



    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<CreateRoleCommand>(command, result));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<UpdateRoleCommand>(command, result));

    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteRoleCommand(id);
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<DeleteRoleCommand>(command, result));
    }
}
