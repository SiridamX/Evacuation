using Evacuation.Domain.Interfaces;
using Evacuation.Infrastructure.Context;
using Microsoft.Extensions.Logging;

namespace Evacuation.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IZoneRepository Zones { get; }
    public IVehicleRepository Vehicles { get; }
    public IAssignmentRepository Assignments { get; }
    public readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(
        ApplicationDbContext context,
        IZoneRepository zones,
        IVehicleRepository vehicles,
        IAssignmentRepository assignments,
        ILogger<UnitOfWork> logger)
    {
        _context = context;
        Zones = zones;
        Vehicles = vehicles;
        Assignments = assignments;
        _logger = logger;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}