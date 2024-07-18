using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infrastructure.Postgres;
using TaskManager.Modules.Users.Infrastructure.Database;

namespace TaskManager.Modules.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPostgres<UsersDbContext>();

        return services;
    }
}