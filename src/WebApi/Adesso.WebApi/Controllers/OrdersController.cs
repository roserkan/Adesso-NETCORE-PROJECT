using Adesso.Application.Constants;
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Mediator.Send(new GetAllOrdersQuerie());
        var result = new SuccessDataResult<List<OrderDto>>(data);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Mediator.Send(new GetOrderByIdQuerie(id));
        var result = new SuccessDataResult<OrderDto>(data);
        return Ok(result);
    }



    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        await Mediator.Send(command); // return type: CreatedOrderDto
        var result = new SuccessResult(Messages.OrderSuccess);
        return Created("", result);
    }

}
