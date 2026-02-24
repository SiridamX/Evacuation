namespace Evacuation.Application.Features.Vehicles.Commands.Create.DTOs;

public class CreateVehicleResponse
{
    public string VehicleId { get; set; } = default!;

    public int Capacity { get; set; }

    public string Type { get; set; } = string.Empty;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double Speed { get; set; }

    public DateTime CreatedAt { get; set; }
}