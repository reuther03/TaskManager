using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Management.Application.Database.Abstractions;
using TaskManager.Modules.Management.Domain.ManagementUsers;
using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Domain.TeamMembers;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Infrastructure.Database;

internal sealed class ManagementsDbContext : DbContext, IManagementsDbContext
{
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<ManagementUser> Users => Set<ManagementUser>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();


    public ManagementsDbContext()
    {
    }

    public ManagementsDbContext(DbContextOptions<ManagementsDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("management");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}