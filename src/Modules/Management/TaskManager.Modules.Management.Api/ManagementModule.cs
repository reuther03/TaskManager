using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Abstractions.Modules;
using TaskManager.Modules.Management.Application;
using TaskManager.Modules.Management.Infrastructure;
using TaskManager.Modules.Users.Domain;

namespace TaskManager.Modules.Management.Api;

internal class ManagementModule : IModule
{
    public const string BasePath = "managements-module";

    public string Name { get; } = "Managements";
    public string Path => BasePath;

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