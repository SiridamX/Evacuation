using Evacuation.Domain.Entities;

namespace Evacuation.Application.Features.Zones.Commands.Create.DTOs;

public class CreateZoneRequest
{
    public string ZoneId { get; set; } = string.Empty;

    public Location LocationCoordinates { get; set; } = default!;

    public int NumberOfPeople { get; set; }

    public int UrgencyLevel { get; set; }

}
