namespace Evacuation.Application.Features.Zones.Commands.Create.DTOs;

public class ZoneResponse
{
    public string ZoneId { get; set; } = default!;


    public int PeopleCount { get; set; }

    public int TotalEvacuated { get; set; }

    public int RemainingPeople { get; set; }
}