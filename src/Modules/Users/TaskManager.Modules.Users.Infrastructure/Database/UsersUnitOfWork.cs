using TaskManager.Infrastructure.Postgres;
using TaskManager.Modules.Users.Application.Abstractions;

namespace TaskManager.Modules.Users.Infrastructure.Database;

internal class UsersUnitOfWork : BaseUnitOfWork<UsersDbContext>, IUsersUnitOfWork
{
    public UsersUnitOfWork(UsersDbContext dbContext) : base(dbContext)
    {
    }
}