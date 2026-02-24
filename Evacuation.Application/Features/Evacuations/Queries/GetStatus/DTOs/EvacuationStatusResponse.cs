namespace Evacuation.Application.Features.Evacuation.Queries.GetStatus.DTOs;

public class EvacuationStatusResponse
{
    public string ZoneId { get; set; } = default!;

    public int TotalEvacuated { get; set; }

    public int RemainingPeople { get; set; }

    public string? LastVehicleUsed { get; set; }
}