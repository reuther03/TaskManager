using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Modules.Groups.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}