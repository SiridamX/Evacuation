using Evacuation.Domain.Entities;
using Evacuation.Domain.Interfaces;
using Evacuation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly ApplicationDbContext _context;

    public AssignmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(VehicleAssignment assignment, CancellationToken cancellationToken)
    {
        await _context.Assignments.AddAsync(assignment, cancellationToken);
    }

    public async Task<List<VehicleAssignment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Assignments
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task ClearAsync(CancellationToken cancellationToken)
    {
        var all = await _context.Assignments.ToListAsync(cancellationToken);
        _context.Assignments.RemoveRange(all);
    }
}