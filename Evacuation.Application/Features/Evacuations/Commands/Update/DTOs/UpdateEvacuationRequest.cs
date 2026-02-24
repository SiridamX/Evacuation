namespace Evacuation.Application.Features.Evacuation.Commands.Update.DTOs;

public class UpdateEvacuationRequest
{
    public required string ZoneId { get; set; }
    public required string VehicleId { get; set; }
    public int PeopleMoved { get; set; }
}