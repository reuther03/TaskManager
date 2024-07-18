using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Users.Domain.Users.Entities;

namespace TaskManager.Modules.Users.Application.Abstractions.Database;

public interface IUserDbContext
{
    DbSet<User> Users { get; }
}