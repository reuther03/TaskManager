using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Modules.Groups.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}