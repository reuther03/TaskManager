using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Modules.Managment.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}