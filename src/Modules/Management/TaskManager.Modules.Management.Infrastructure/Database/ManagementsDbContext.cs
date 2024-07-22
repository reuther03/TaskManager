using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Management.Application.Database.Abstractions;
using TaskManager.Modules.Management.Domain.Groups.Entities;

namespace TaskManager.Modules.Management.Infrastructure.Database;

internal class ManagementsDbContext : DbContext, IManagementsDbContext
{
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TeamTask> Tasks => Set<TeamTask>();
    public DbSet<TeamUser> Users => Set<TeamUser>();

    public ManagementsDbContext()
    {
    }

    public ManagementsDbContext(DbContextOptions<ManagementsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("management");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}