using TaskManager.Bootstrapper;
using TaskManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;


services.AddEndpointsApiExplorer();

var assemblies = ModuleLoader.LoadAssemblies(configuration);
var modules = ModuleLoader.LoadModules(assemblies);

services.AddInfrastructure(assemblies, modules);

foreach (var module in modules)
{
    module.Register(services);
}

var app = builder.Build();

app.UseInfrastructure();
foreach (var module in modules)
{
    module.Use(app);
}

app.MapControllers();

app.Run();