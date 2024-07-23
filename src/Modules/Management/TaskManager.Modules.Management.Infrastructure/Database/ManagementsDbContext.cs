using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Management.Application.Database.Abstractions;
using TaskManager.Modules.Management.Domain.ManagementUsers;
using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Infrastructure.Database;

internal class ManagementsDbContext : DbContext, IManagementsDbContext
{
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<ManagementUser> Users => Set<ManagementUser>();

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