using TaskManager.Infrastructure.Postgres;
using TaskManager.Modules.Management.Application.Database;

namespace TaskManager.Modules.Management.Infrastructure.Database;

internal class ManagementUnitOfWork : BaseUnitOfWork<ManagementsDbContext>, IUnitOfWork
{
    public ManagementUnitOfWork(ManagementsDbContext dbContext) : base(dbContext)
    {
    }
}