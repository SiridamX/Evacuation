using Evacuation.Domain.Common;

namespace Evacuation.Domain.Entities;

public class VehicleAssignment : BaseEntity
{
    public string ZoneId { get; set; } = default!;
    public string VehicleId { get; set; } = default!;
    public int AssignedPeople { get; set; }
    public double EstimatedArrival { get; set; }

    //nav
    public Zone Zone { get; set; } = default!;
    public Vehicle Vehicle { get; set; } = default!;
}