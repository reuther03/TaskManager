using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Modules.Managment.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}