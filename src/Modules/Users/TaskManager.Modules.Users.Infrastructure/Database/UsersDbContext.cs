using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Users.Application.Abstractions.Database;
using TaskManager.Modules.Users.Domain.Users.Entities;

namespace TaskManager.Modules.Users.Infrastructure.Database;

internal class UsersDbContext : DbContext, IUserDbContext
{
    public DbSet<User> Users => Set<User>();

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}