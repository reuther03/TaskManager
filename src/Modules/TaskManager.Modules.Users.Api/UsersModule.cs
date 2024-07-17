using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Abstractions.Modules;
using TaskManager.Modules.Users.Application;
using TaskManager.Modules.Users.Domain;
using TaskManager.Modules.Users.Infrastructure;

namespace TaskManager.Modules.Users.Api;

public class UsersModule : IModule
{
    public string Name => nameof(UsersModule);
    public string Path => "users-module";
    public void Register(IServiceCollection services)
    {
        services
            .AddDomain()
            .AddApplication()
            .AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}