using Evacuation.Application.Features.Evacuation.Queries.GetStatus.DTOs;

namespace Evacuation.Application.Common.Interfaces;
public interface IRedisStore
{
    Task SetStatusAsync(EvacuationStatusResponse status);
    Task<EvacuationStatusResponse?> GetStatusAsync(string zoneId);
}