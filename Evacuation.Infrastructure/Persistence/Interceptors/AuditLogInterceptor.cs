using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

public class AuditLogInterceptor : SaveChangesInterceptor
{
    private readonly ILogger<AuditLogInterceptor> _logger;

    public AuditLogInterceptor(ILogger<AuditLogInterceptor> logger)
    {
        _logger = logger;
    }
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context == null) return result;

        var entries = context.ChangeTracker.Entries()
            .Where(e =>
                e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted);

        foreach (var entry in entries)
        {
            var entityName = entry.Entity.GetType().Name;
            var tableName = entry.Metadata.GetTableName();

            _logger.LogInformation(
                $"Trace: {entry.State} | Entity: {entityName} | Table: {tableName}");
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}