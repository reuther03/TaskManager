using TaskManager.Abstractions.Kernel.Primitives.Result;

namespace TaskManager.Abstractions.Kernel.Database;

public interface IBaseUnitOfWork
{
    Task<Result> CommitAsync(CancellationToken cancellationToken = default);
}