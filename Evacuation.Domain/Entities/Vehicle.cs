using Evacuation.Domain.Common;

namespace Evacuation.Domain.Entities;

public class Vehicle : BaseEntity
{
    public string VehicleId { get; set; } = default!;

    public int Capacity { get; set; }

    public string Type { get; set; } = default!;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double Speed { get; set; }

    public bool IsAvailable { get; set; }

    private Vehicle() { }

    public Vehicle(
        string vehicleId, int capacity, string type, double latitude, double longitude, double speed)
    {
        VehicleId = vehicleId;
        Capacity = capacity;
        Type = type;
        Latitude = latitude;
        Longitude = longitude;
        Speed = speed;
        IsAvailable = true;
    }

    public void MarkUnavailable()
    {
        if (!IsAvailable)
            throw new InvalidOperationException("Vehicle already assigned.");

        IsAvailable = false;
        SetUpdated();
    }

    public void MarkAvailable()
    {
        IsAvailable = true;
        SetUpdated();
    }

    public void UpdateLocation(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
        SetUpdated();
    }
}