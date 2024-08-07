using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Kernel.Database;
using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.Primitives.Result;

namespace TaskManager.Infrastructure.Postgres;

public abstract class BaseUnitOfWork<T> : IBaseUnitOfWork where T : DbContext
{
    private readonly T _context;
    private readonly IPublisher _publisher;

    protected BaseUnitOfWork(T context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public virtual async Task<Result> CommitAsync(CancellationToken cancellationToken = default)
    {
        bool commitStatus;

        try
        {
            var changes =  await _context.SaveChangesAsync(cancellationToken);
            if (changes <= 0)
                return Result.InternalServerError("An error occurred while saving changes to the database.");

            await PublishDomainEvents();
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

    private async Task PublishDomainEvents()
    {
        var domainEntities = _context.ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(x => x.Entity.DomainEvents.Count > 0)
            .ToList();

        foreach (var entity in domainEntities)
        {
            var events = entity.Entity.DomainEvents.ToList();
            entity.Entity.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}