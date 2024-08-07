using MediatR;
using TaskManager.Infrastructure.Postgres;
using TaskManager.Modules.Users.Application.Abstractions;

namespace TaskManager.Modules.Users.Infrastructure.Database;

internal class UserUnitOfWork : BaseUnitOfWork<UsersDbContext>, IUnitOfWork
{
    public UserUnitOfWork(UsersDbContext dbContext, IPublisher publisher) : base(dbContext, publisher)
    {
    }
}