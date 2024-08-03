using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Abstractions.Email;
using TaskManager.Abstractions.Modules;
using TaskManager.Infrastructure.Api;
using TaskManager.Infrastructure.Auth;
using TaskManager.Infrastructure.Postgres;
using TaskManager.Infrastructure.Services;
using TaskManager.Infrastructure.Services.CloudinaryImg;
using TaskManager.Infrastructure.Swagger;

[assembly: InternalsVisibleTo("TaskManager.Bootstrapper")]

namespace TaskManager.Infrastructure;

internal static class Extensions
{
    private const string CorsPolicy = "cors";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IList<Assembly> assemblies, IList<IModule> modules,
        IConfiguration configuration)
    {
        var disabledModules = new List<string>();
        // using (var serviceProvider = services.BuildServiceProvider())
        {
            // var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            foreach (var (key, value) in configuration.AsEnumerable())
            {
                if (!key.Contains(":module:enabled"))
                {
                    continue;
                }

                if (!bool.Parse(value))
                {
                    disabledModules.Add(key.Split(":")[0]);
                }
            }
        }

        services.AddCors(cors =>
        {
            cors.AddPolicy(CorsPolicy, x =>
            {
                x.WithOrigins("*")
                    .WithMethods("POST", "PUT", "DELETE")
                    .WithHeaders("Content-Type", "Authorization");
            });
        });

        services.AddCloudinary(configuration);
        services.AddSwagger();
        services.AddAuth(configuration);
        services.AddDecorators();
        services.AddHostedService<AppInitializer>();
        services.Configure<EmailSettings>(configuration.GetRequiredSection(EmailSettings.SectionName));
        services.AddServices();
        services.AddPostgres();
        services.AddMediatrWithFilters(assemblies);

        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                var removedParts = new List<ApplicationPart>();
                foreach (var disabledModule in disabledModules)
                {
                    var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule, StringComparison.InvariantCultureIgnoreCase));
                    removedParts.AddRange(parts);
                }

                foreach (var part in removedParts)
                {
                    manager.ApplicationParts.Remove(part);
                }

                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicy);
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Manager API"); });
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        return app;
    }

    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }

    public static string GetModuleName(this object value)
        => value?.GetType().GetModuleName() ?? string.Empty;

    public static string GetModuleName(this Type type)
    {
        if (type?.Namespace is null)
        {
            return string.Empty;
        }

        return type.Namespace.StartsWith("TaskManager.Modules.")
            ? type.Namespace.Split('.')[2].ToLowerInvariant()
            : string.Empty;
    }
}