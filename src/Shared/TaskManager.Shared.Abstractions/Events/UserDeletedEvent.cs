using MediatR;
using TaskManager.Abstractions.Kernel.ValueObjects.User;

namespace TaskManager.Abstractions.Events;

public record UserDeletedEvent(UserId UserId) : INotification;