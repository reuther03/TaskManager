using MediatR;

namespace TaskManager.Abstractions.Events;

public record UserCreated(Guid UserId, string FullName, string Email) : INotification;