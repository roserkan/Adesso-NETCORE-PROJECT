using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace Adesso.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    private IMediator? _mediator;



    public int? Id
    {
        get
        {
            var val = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var accessToken = Request.Headers[HeaderNames.Authorization];
            return val is null ? null : Int32.Parse(val);
        }
    }
}
