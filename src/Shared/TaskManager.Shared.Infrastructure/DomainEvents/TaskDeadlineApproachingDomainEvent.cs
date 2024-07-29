using TaskManager.Abstractions.Kernel;

namespace TaskManager.Infrastructure.DomainEvents;

public class TaskDeadlineApproachingDomainEvent(
    Guid TeamId,
    Guid TaskId,
    Guid AssignedUserId
) : IDomainEvent;