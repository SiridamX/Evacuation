using Microsoft.EntityFrameworkCore;
using Evacuation.Domain.Entities;
using Evacuation.Domain.Interfaces;
using Evacuation.Infrastructure.Context;

namespace Evacuation.Infrastructure.Persistence;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _context;

    public VehicleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
    {
        await _context.Vehicles.AddAsync(vehicle, cancellationToken);
    }

    public async Task<Vehicle?> GetByIdAsync(string vehicleId, CancellationToken cancellationToken)
    {
        return await _context.Vehicles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.VehicleId == vehicleId, cancellationToken);
    }

    public async Task<List<Vehicle>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Vehicles
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}