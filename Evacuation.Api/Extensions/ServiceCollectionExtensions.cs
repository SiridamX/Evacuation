using Evacuation.Application.Features.Vehicles.Commands.Create;
using Evacuation.Infrastructure.DependencyInjection;

namespace Evacuation.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(
                typeof(CreateVehicleCommand).Assembly
            );
        });

        // mapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
