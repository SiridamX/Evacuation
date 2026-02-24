using Microsoft.EntityFrameworkCore;
using Evacuation.Domain.Entities;
using Evacuation.Domain.Interfaces;
using Evacuation.Infrastructure.Context;

namespace Evacuation.Infrastructure.Persistence;

public class ZoneRepository : IZoneRepository
{
    private readonly ApplicationDbContext _context;

    public ZoneRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Zone zone, CancellationToken cancellationToken)
    {
        await _context.Zones.AddAsync(zone, cancellationToken);
    }

    public async Task<Zone?> GetByIdAsync(string zoneId, CancellationToken cancellationToken)
    {
        return await _context.Zones
            .FirstOrDefaultAsync(x => x.ZoneId == zoneId, cancellationToken);
    }

    public async Task<List<Zone>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Zones
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public Task UpdateAsync(Zone zone, CancellationToken cancellationToken)
    {
        _context.Zones.Update(zone);
        return Task.CompletedTask;
    }

    public async Task ResetAllAsync(CancellationToken cancellationToken)
    {
        var zones = await _context.Zones.ToListAsync(cancellationToken);

        foreach (var zone in zones)
        {
            zone.Reset();
        }
    }
}