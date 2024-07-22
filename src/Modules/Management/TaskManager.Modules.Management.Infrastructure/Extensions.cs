using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infrastructure.Postgres;
using TaskManager.Modules.Management.Infrastructure.Database;

namespace TaskManager.Modules.Management.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres<ManagementsDbContext>();

        return services;
    }
}