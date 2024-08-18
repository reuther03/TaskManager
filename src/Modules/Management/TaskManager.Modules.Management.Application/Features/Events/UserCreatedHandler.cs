using MediatR;
using TaskManager.Abstractions.Events;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.ManagementUsers;

namespace TaskManager.Modules.Management.Application.Features.Events;

public class UserCreatedHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly IManagementUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserCreatedHandler(IManagementUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsAsync(new UserId(notification.UserId), cancellationToken))
            return;

        var user = ManagementUser.Create(new UserId(notification.UserId), new Name(notification.FullName), new Email(notification.Email));

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}