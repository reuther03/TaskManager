using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Modules.Users.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}