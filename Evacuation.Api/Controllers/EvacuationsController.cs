using Evacuation.Application.Features.Evacuation.Commands.GeneratePlan;
using Evacuation.Application.Features.Evacuation.Queries.GetStatus;
using Evacuation.Application.Features.Evacuation.Reset;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Evacuation.API.Controllers;

[ApiController]
[Route("api/evacuations")]
public class EvacuationController : ControllerBase
{
    private readonly IMediator _mediator;

    public EvacuationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("plan")]
    public async Task<IActionResult> GeneratePlan()
    {
        var result = await _mediator.Send(
            new GenerateEvacuationPlanCommand());

        return Ok(result);
    }
    [HttpGet("status")]
    public async Task<IActionResult> GetStatus()
    {
        var result = await _mediator.Send(new GetEvacuationStatusQuery());
        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(
    [FromBody] UpdateEvacuationCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("clear")]
    public async Task<IActionResult> Clear()
    {
        await _mediator.Send(new ResetEvacuationCommand());
        return NoContent();
    }

    [HttpPost("test")]
    public async Task<IActionResult> Test()
    {
        int[] numbers = new[] { 1, 1, 2, 3, 4, 5 };

        var result = numbers
            .GroupBy(z => z)
            .Select(g => new ZoneCountResponse
            {
                Number = g.Key,
                Count = g.Count()
            })
            .ToList();

        return Ok(result);
    }

    public class ZoneCountResponse
    {
        public int Number { get; set; }
        public int Count { get; set; }
    }
}