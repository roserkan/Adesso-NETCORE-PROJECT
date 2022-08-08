using Adesso.Application.Dtos.Order;
using Adesso.Application.Features.Order.Commands.Create;
using Adesso.Application.Features.Order.Queries;
using Adesso.Application.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : BaseController
{

    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var result = await _mediator.Send(new GetAllOrdersQuerie());
        return Ok(new SuccessDataResult<List<OrderDto>>(result));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuerie(id));
        return Ok(new SuccessDataResult<OrderDto>(result));
    }



    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new SuccessDataResult<CreateOrderCommand>(command, result));
    }

    //[HttpPut("{id:int}")]
    //public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderCommand command)
    //{
    //    command.Id = id;
    //    var result = await _mediator.Send(command);
    //    return Ok(new SuccessDataResult<UpdateOrderCommand>(command, result));

    //}


    //[HttpDelete("{id:int}")]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    var command = new DeleteOrderCommand(id);
    //    var result = await _mediator.Send(command);
    //    return Ok(new SuccessDataResult<DeleteOrderCommand>(command, result));
    //}
}
