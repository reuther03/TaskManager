﻿using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infrastructure.Postgres;
using TaskManager.Modules.Users.Application.Abstractions;
using TaskManager.Modules.Users.Application.Abstractions.Database;
using TaskManager.Modules.Users.Application.Abstractions.Database.Repositories;
using TaskManager.Modules.Users.Infrastructure.Database;
using TaskManager.Modules.Users.Infrastructure.Database.Repositories;

namespace TaskManager.Modules.Users.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPostgres<UsersDbContext>()
            .AddScoped<IUserDbContext, UsersDbContext>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddUnitOfWork<IUnitOfWork, UserUnitOfWork>();

        return services;
    }
}