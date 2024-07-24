using TaskManager.Infrastructure.Postgres;
using TaskManager.Modules.Management.Application.Database;

namespace TaskManager.Modules.Management.Infrastructure.Database;

internal class UnitOfWork : BaseUnitOfWork<ManagementsDbContext>, IUnitOfWork
{
    public UnitOfWork(ManagementsDbContext dbContext) : base(dbContext)
    {
    }
}