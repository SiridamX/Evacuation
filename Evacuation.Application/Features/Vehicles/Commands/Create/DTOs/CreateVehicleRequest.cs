using Evacuation.Domain.Entities;

namespace Evacuation.Application.Features.Vehicles.Commands.Create.DTOs;

public class CreateVehicleRequest
{
    
    public int Capacity { get; set; }

    public string Type { get; set; } = string.Empty;

    public required Location LocationCoordinates { get; set; }

    public double Speed { get; set; }
    public string VehicleId { get;  set; } = default!;
}
