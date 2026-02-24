using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Evacuation.API.Controllers;

[ApiController]
[Route("api/zones")]
public class ZonesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ZonesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateZone(
        [FromBody] CreateZoneCommand command)

    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}