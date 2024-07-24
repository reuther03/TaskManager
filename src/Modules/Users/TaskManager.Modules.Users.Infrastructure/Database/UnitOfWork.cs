using TaskManager.Infrastructure.Postgres;
using TaskManager.Modules.Users.Application.Abstractions;

namespace TaskManager.Modules.Users.Infrastructure.Database;

internal class UnitOfWork : BaseUnitOfWork<UsersDbContext>, IUnitOfWork
{
    public UnitOfWork(UsersDbContext dbContext) : base(dbContext)
    {
    }
}