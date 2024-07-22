using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Modules.Management.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}