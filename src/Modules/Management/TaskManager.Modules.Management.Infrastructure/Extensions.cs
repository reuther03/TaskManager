using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infrastructure.Postgres;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Infrastructure.Database;
using TaskManager.Modules.Management.Infrastructure.Database.Repositories;

namespace TaskManager.Modules.Management.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPostgres<ManagementsDbContext>()
            .AddScoped<ITeamRepository, TeamRepository>()
            .AddScoped<IManagementUserRepository, ManagementUserRepository>()
            .AddScoped<ITeamMemberRepository, TeamMemberRepository>()
            .AddUnitOfWork<IUnitOfWork, ManagementUnitOfWork>();

        return services;
    }
}