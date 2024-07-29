using MediatR;

namespace TaskManager.Abstractions.Events;

public record UserLoggedInEvent(Guid UserId, string Email) : INotification;