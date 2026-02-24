using System.Text.Json;
using Evacuation.Application.Common.Interfaces;
using Evacuation.Application.Features.Evacuation.Queries.GetStatus.DTOs;
using Evacuation.Domain.Entities;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Evacuation.Infrastructure.Common;

public class RedisStore : IRedisStore
{
    private readonly IDatabase _db;

    private readonly ILogger<RedisStore> _logger;

    public RedisStore(IConnectionMultiplexer redis, ILogger<RedisStore> logger)
    {
        _db = redis.GetDatabase();
        _logger = logger;
    }

    public async Task SetStatusAsync(EvacuationStatusResponse status)
    {
        var key = $"zone-status:{status.ZoneId}";
        _logger.LogInformation($"[Redis]Set Zone : {key}");
        await _db.StringSetAsync(
            key,
            JsonSerializer.Serialize(status));
    }

    public async Task<EvacuationStatusResponse?> GetStatusAsync(string zoneId)
    {
        var value = await _db.StringGetAsync(
            $"zone-status:{zoneId}");
        _logger.LogInformation($"[Redis]Get Zone-Status : {value}");
        if (value.IsNullOrEmpty)
            return null;

        return JsonSerializer.Deserialize<EvacuationStatusResponse>(value!);
    }
}