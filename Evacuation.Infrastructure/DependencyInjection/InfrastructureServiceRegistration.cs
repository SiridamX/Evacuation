using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Evacuation.Infrastructure.Context;
using Evacuation.Infrastructure.Persistence;
using Evacuation.Domain.Interfaces;
using Evacuation.Application.Common.Interfaces;
using Evacuation.Infrastructure.Common;
using StackExchange.Redis;

namespace Evacuation.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuditLogInterceptor>();
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")).AddInterceptors(sp.GetRequiredService<AuditLogInterceptor>()));

        services.AddScoped<IZoneRepository, ZoneRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();
        services.AddScoped<IRedisStore, RedisStore>();
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var config = ConfigurationOptions.Parse(
                configuration.GetConnectionString("Redis"),
                true);

            config.AbortOnConnectFail = false;

            return ConnectionMultiplexer.Connect(config);
        });

        services.AddScoped<IRedisStore, RedisStore>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}