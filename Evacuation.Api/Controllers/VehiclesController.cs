using MediatR;
using Microsoft.AspNetCore.Mvc;
using Evacuation.Application.Features.Vehicles.Commands.Create;
using Evacuation.Application.Features.Vehicles.Commands.Create.DTOs;

namespace Evacuation.API.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehiclesController(IMediator mediator)
    {
        _mediator = mediator;
    }

  [HttpPost]
public async Task<IActionResult> CreateVehicles(
    [FromBody] CreateVehicleCommand command)
{
    var result = await _mediator.Send(command);
    return Ok(result);
}
}