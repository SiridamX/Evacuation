using Evacuation.Domain.Entities;

namespace Evacuation.Domain.Interfaces;

public interface IZoneRepository
{
    Task AddAsync(Zone zone, CancellationToken cancellationToken);

    Task<Zone?> GetByIdAsync(string zoneId, CancellationToken cancellationToken);

    Task<List<Zone>> GetAllAsync(CancellationToken cancellationToken);

    Task UpdateAsync(Zone zone, CancellationToken cancellationToken);

    Task ResetAllAsync(CancellationToken cancellationToken);
}