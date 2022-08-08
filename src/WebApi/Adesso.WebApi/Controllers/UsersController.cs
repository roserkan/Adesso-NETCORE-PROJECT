using Adesso.Application.Dtos.User;
using Adesso.Application.Features.Commands.User.Create;
using Adesso.Application.Features.Commands.User.Delete;
using Adesso.Application.Features.Commands.User.Login;
using Adesso.Application.Features.Commands.User.Update;
using Adesso.Application.Features.Queries.User;
using Adesso.Application.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{

    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _mediator.Send(new GetAllUsersQuerie());
        return Ok(new SuccessDataResult<List<UserDto>>(result));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await _mediator.Send(new GetUserByIdQuerie(id));
        return Ok(new SuccessDataResult<UserDto>(result));
    }



    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<CreateUserCommand>(command, result));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<UpdateUserCommand>(command, result));

    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteUserCommand(id);
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<DeleteUserCommand>(command, result));
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<LoginUserDto>(result));
    }
}