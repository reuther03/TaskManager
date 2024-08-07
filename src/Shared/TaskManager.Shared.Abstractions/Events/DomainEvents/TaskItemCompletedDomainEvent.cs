using TaskManager.Abstractions.Kernel.Events;

namespace TaskManager.Abstractions.Events.DomainEvents;

public sealed record TaskItemCompletedDomainEvent(Guid TaskId) : IDomainEvent;