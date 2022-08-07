using Adesso.Application.Features.Commands.Order.Create;
using Adesso.Application.Features.Queries.Order;
using Adesso.Application.Utilities.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{

    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    //[SecuredOperation("Admin")]
    public async Task<IActionResult> GetAllOrders()
    {
        var result = await _mediator.Send(new GetAllOrdersQuerie());
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuerie(id));
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }



    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    //[HttpPut("{id:int}")]
    //public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderCommand command)
    //{
    //    command.Id = id;
    //    var result = await _mediator.Send(command);

    //    if (result.Success)
    //    {
    //        return Ok(result);
    //    }
    //    return BadRequest(result);
    //}


    //[HttpDelete("{id:int}")]
    //public async Task<IActionResult> DeleteOrder(int id)
    //{
    //    var command = new DeleteOrderCommand(id);
    //    var result = await _mediator.Send(command);

    //    if (result.Success)
    //    {
    //        return Ok(result);
    //    }
    //    return BadRequest(result);
    //}
}
