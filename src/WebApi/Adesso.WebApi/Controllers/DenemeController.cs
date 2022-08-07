using Adesso.Application.Features.Commands.Order.Create;
using Adesso.Application.Features.Commands.User.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adesso.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenemeController : BaseController
    {
        private readonly IMediator mediator;

        public DenemeController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public  async Task<IActionResult> UpdateUser([FromBody] CreateOrderCommand command)
        {

            var result = await mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
