using TaskManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpointsApiExplorer();

services.AddInfrastructure();

var app = builder.Build();

app.UseInfrastructure();

await app.RunAsync();