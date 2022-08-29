using Adesso.Application.Constants;
using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Features.UserDetail.Commands.Create;
using Adesso.Application.Features.UserDetail.Commands.Delete;
using Adesso.Application.Features.UserDetail.Commands.Update;
using Adesso.Application.Features.UserDetail.Queries;
using Adesso.Application.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserDetailsController : BaseController
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Mediator.Send(new GetAllUserDetailsQuerie());
        var result = new SuccessDataResult<List<UserDetailDto>>(data);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Mediator.Send(new GetUserDetailByIdQuerie(id));
        var result = new SuccessDataResult<UserDetailDto>(data);
        return Ok(result);
    }

    [HttpGet("userId")]
    public async Task<IActionResult> GetByUserId(int id)
    {
        var data = await Mediator.Send(new GetUserDetailByUserIdQuerie(id));
        var result = new SuccessDataResult<UserDetailDto>(data);
        return Ok(result);
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDetailCommand command)
    {
        await Mediator.Send(command); // return type: CreatedUserDetailDto
        var result = new SuccessResult(Messages.UserDetailCreated);
        return Created("", result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDetailCommand command)
    {
        command.Id = id;
        await Mediator.Send(command); // return type: UpdatedUserDetailDto
        var result = new SuccessResult(Messages.UserDetailUpdated);
        return Ok(result);
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteUserDetailCommand(id);
        await Mediator.Send(command); // return type: DeletedUserDetailDto
        var result = new SuccessResult(Messages.UserDetailDeleted);
        return Ok(result);
    }
}
