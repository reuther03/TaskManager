using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Kernel;
using TaskManager.Abstractions.Kernel.Primitives.Result;

namespace TaskManager.Infrastructure.Postgres;

public abstract class BaseUnitOfWork<T> : IBaseUnitOfWork where T : DbContext
{
    private readonly T _context;

    protected BaseUnitOfWork(T context)
    {
        _context = context;
    }

    public async Task<Result> CommitAsync(CancellationToken cancellationToken = default)
    {
        bool commitStatus;

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            commitStatus = true;
        }
        catch (Exception)
        {
            commitStatus = false;
        }

        return commitStatus
            ? Result.Ok()
            : Result.InternalServerError("An error occurred while saving changes to the database.");
    }
}