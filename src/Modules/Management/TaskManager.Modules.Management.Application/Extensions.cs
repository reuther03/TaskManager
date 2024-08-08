using Microsoft.Extensions.DependencyInjection;
using TaskManager.Modules.Management.Application.Workflows;

namespace TaskManager.Modules.Management.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}