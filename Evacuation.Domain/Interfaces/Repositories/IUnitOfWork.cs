namespace Evacuation.Domain.Interfaces;

public interface IUnitOfWork
{
    IZoneRepository Zones { get; }
    IVehicleRepository Vehicles { get; }
    IAssignmentRepository Assignments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}