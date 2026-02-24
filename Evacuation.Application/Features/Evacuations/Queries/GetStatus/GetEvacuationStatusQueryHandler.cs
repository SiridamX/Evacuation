using MediatR;
using Evacuation.Application.Features.Evacuation.Queries.GetStatus.DTOs;
using Evacuation.Domain.Interfaces;
using Evacuation.Application.Common.Interfaces;
using Evacuation.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Evacuation.Application.Features.Evacuation.Queries.GetStatus;

public class GetEvacuationStatusQueryHandler
    : IRequestHandler<GetEvacuationStatusQuery, List<EvacuationStatusResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRedisStore _redis;
    private readonly ILogger<GetEvacuationStatusQueryHandler> _logger;

    public GetEvacuationStatusQueryHandler(IUnitOfWork unitOfWork, IRedisStore redis,ILogger<GetEvacuationStatusQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _redis = redis;
        _logger = logger;
    }

    public async Task<List<EvacuationStatusResponse>> Handle(
    GetEvacuationStatusQuery request,
    CancellationToken cancellationToken)
    {
        var zones = await _unitOfWork.Zones.GetAllAsync(cancellationToken);

        var result = new List<EvacuationStatusResponse>();

        foreach (var zone in zones)
        {
            var cacheKey = $"zone-status:{zone.ZoneId}";
            var cached = await _redis.GetStatusAsync(cacheKey);
            _logger.LogInformation($"[Redis] Key ->{cacheKey} : {cached}");
            if (cached != null)
            {
                result.Add(cached);
                continue;
            }

            var status = new EvacuationStatusResponse
            {
                ZoneId = zone.ZoneId,
                TotalEvacuated = zone.EvacuatedPeople,
                RemainingPeople = zone.RemainingPeople,
                LastVehicleUsed = zone.LastVehicleUsed
            };

            await _redis.SetStatusAsync(status);

            result.Add(status);
        }

        return result;
    }
}