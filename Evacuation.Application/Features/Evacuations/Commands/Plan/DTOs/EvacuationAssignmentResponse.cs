namespace Evacuation.Application.Features.Evacuation.Commands.GeneratePlan.DTOs;

public class EvacuationAssignmentResponse
{
    public string ZoneId { get; set; } = default!;

    public string VehicleId { get; set; } = default!;

    public string ETA { get; set; } = default!;

    public int NumberOfPeople { get; set; }
}