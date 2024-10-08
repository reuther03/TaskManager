﻿using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infrastructure.Postgres;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Abstractions;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Application.Workflows;
using TaskManager.Modules.Management.Infrastructure.Database;
using TaskManager.Modules.Management.Infrastructure.Database.Repositories;
using TaskManager.Modules.Management.Infrastructure.Jobs;
using TaskManager.Modules.Management.Infrastructure.Workflows;

namespace TaskManager.Modules.Management.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPostgres<ManagementsDbContext>()
            .AddScoped<IManagementsDbContext, ManagementsDbContext>()
            .AddScoped<ITeamRepository, TeamRepository>()
            .AddScoped<IManagementUserRepository, ManagementUserRepository>()
            .AddScoped<ITeamMemberRepository, TeamMemberRepository>()
            .AddScoped<ITaskRepository, TaskRepository>()
            .AddScoped<ITeamFileRepository, TeamFileRepository>()
            .AddUnitOfWork<IUnitOfWork, ManagementUnitOfWork>()
            .AddScoped<IWorkflowEngine, WorkflowEngine>();

        services.AddHostedService<TaskDeadlineReminderJob>();

        return services;
    }
}