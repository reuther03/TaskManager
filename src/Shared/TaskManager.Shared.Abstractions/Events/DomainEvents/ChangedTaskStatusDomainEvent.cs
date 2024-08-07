using TaskManager.Abstractions.Kernel.Events;

namespace TaskManager.Abstractions.Events.DomainEvents;

public sealed record ChangedTaskStatusDomainEvent(Guid TaskId) : IDomainEvent;