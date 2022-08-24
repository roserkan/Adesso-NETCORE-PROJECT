using Adesso.Application.Constants;
using Adesso.Application.Dtos.User;
using Adesso.Application.Features.User.Commands.Create;
using Adesso.Application.Features.User.Commands.Delete;
using Adesso.Application.Features.User.Commands.Login;
using Adesso.Application.Features.User.Commands.Update;
using Adesso.Application.Features.User.Queries;
using Adesso.Application.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Mediator.Send(new GetAllUsersQuerie());
        var result = new SuccessDataResult<List<UserDto>>(data);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Mediator.Send(new GetUserByIdQuerie(id));
        var result = new SuccessDataResult<UserDto>(data);
        return Ok(result);
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        await Mediator.Send(command); // return type: CreatedUserDto
        var result = new SuccessResult(Messages.UserCreated);
        return Created("", result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
    {
        command.Id = id;
        await Mediator.Send(command); // return type: UpdatedUserDto
        var result = new SuccessResult(Messages.UserUpdated);
        return Ok(result);
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteUserCommand(id);
        await Mediator.Send(command); // return type: DeletedUserDto
        var result = new SuccessResult(Messages.UserDeleted);
        return Ok(result);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(new SuccessDataResult<LoginUserDto>(result));
    }
}