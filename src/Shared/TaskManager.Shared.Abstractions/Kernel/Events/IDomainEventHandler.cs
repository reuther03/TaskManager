using MediatR;

namespace TaskManager.Abstractions.Kernel.Events;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;