using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Users.Domain.Users;

namespace TaskManager.Modules.Users.Application.Abstractions.Database;

public interface IUserDbContext
{
    DbSet<User> Users { get; }
}