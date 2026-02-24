using Evacuation.Domain.Entities;

namespace Evacuation.Domain.Interfaces;

public interface IAssignmentRepository
{
    Task AddAsync(VehicleAssignment assignment, CancellationToken cancellationToken);

    Task<List<VehicleAssignment>> GetAllAsync(CancellationToken cancellationToken);

    Task ClearAsync(CancellationToken cancellationToken);
}