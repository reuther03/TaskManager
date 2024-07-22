using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Management.Domain.Groups.Entities;

namespace TaskManager.Modules.Management.Application.Database.Abstractions;

public interface IManagementsDbContext
{
    DbSet<Team> Teams { get; }
    DbSet<TeamTask> Tasks { get; }
    DbSet<TeamUser> Users { get; }
}