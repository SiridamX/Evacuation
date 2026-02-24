using Evacuation.Domain.Entities;

namespace Evacuation.Domain.Interfaces;

public interface IVehicleRepository
{
    Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);

    Task<Vehicle?> GetByIdAsync(string vehicleId, CancellationToken cancellationToken);

    Task<List<Vehicle>> GetAllAsync(CancellationToken cancellationToken);
}