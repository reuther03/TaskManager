using MediatR;

namespace TaskManager.Abstractions.Events;

public record UserCreatedEvent(Guid UserId, string FullName, string Email, string ProfilePictureUrl) : INotification;