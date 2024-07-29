using TaskManager.Abstractions.Kernel;

namespace TaskManager.Infrastructure.DomainEvents;

public class UserLoggedInDomainEvent(Guid UserId) : IDomainEvent;