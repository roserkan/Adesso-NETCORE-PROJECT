using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Features.Commands.UserDetail.Create;
using Adesso.Application.Features.Commands.UserDetail.Delete;
using Adesso.Application.Features.Commands.UserDetail.Update;
using Adesso.Application.Features.Queries.UserDetail;
using Adesso.Application.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserDetailsController : BaseController
{

    private readonly IMediator _mediator;

    public UserDetailsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUserDetails()
    {
        var result = await _mediator.Send(new GetAllUserDetailsQuerie());
        return Ok(new SuccessDataResult<List<UserDetailDto>>(result));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserDetailById(int id)
    {
        var result = await _mediator.Send(new GetUserDetailByIdQuerie(id));
        return Ok(new SuccessDataResult<UserDetailDto>(result));
    }



    [HttpPost]
    public async Task<IActionResult> CreateUserDetail([FromBody] CreateUserDetailCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<CreateUserDetailCommand>(command, result));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUserDetail(int id, [FromBody] UpdateUserDetailCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<UpdateUserDetailCommand>(command, result));

    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteUserDetailCommand(id);
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<DeleteUserDetailCommand>(command, result));
    }
}
