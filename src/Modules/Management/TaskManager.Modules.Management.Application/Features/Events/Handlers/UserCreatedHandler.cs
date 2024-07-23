using MediatR;
using TaskManager.Abstractions.Events;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.ManagementUsers;

namespace TaskManager.Modules.Management.Application.Features.Events.Handlers;

public class UserCreatedHandler : INotificationHandler<UserCreated>
{
    private readonly IManagementUserRepository _userRepository;

    public UserCreatedHandler(IManagementUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
    {
        if ( await _userRepository.ExistsAsync(new UserId(notification.UserId), cancellationToken))
        {
            return;
        }

        var user = ManagementUser.Create(new UserId(notification.UserId), new Name(notification.FullName), new Email(notification.Email));

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);
    }
}