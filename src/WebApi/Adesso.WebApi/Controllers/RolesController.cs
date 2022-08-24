using Adesso.Application.Constants;
using Adesso.Application.Dtos.Role;
using Adesso.Application.Features.Role.Commands.Create;
using Adesso.Application.Features.Role.Commands.Delete;
using Adesso.Application.Features.Role.Commands.Update;
using Adesso.Application.Features.Role.Queries;
using Adesso.Application.Utilities.Results;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : BaseController
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Mediator.Send(new GetAllRolesQuerie());
        var result = new SuccessDataResult<List<RoleDto>>(data);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Mediator.Send(new GetRoleByIdQuerie(id));
        var result = new SuccessDataResult<RoleDto>(data);
        return Ok(result);
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        await Mediator.Send(command); // return type: CreatedRoleDto
        var result = new SuccessResult(Messages.RoleCreated);
        return Created("", result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleCommand command)
    {
        command.Id = id;
        await Mediator.Send(command); // return type: UpdatedRoleDto
        var result = new SuccessResult(Messages.RoleUpdated);
        return Ok(result);
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteRoleCommand(id);
        await Mediator.Send(command); // return type: DeletedRoleDto
        var result = new SuccessResult(Messages.RoleDeleted);
        return Ok(result);
    }
}
