using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Domain.ManagementUsers;

namespace TaskManager.Modules.Management.Application.Database.Repositories;

public interface IManagementUserRepository
{
    Task<ManagementUser?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(UserId id, CancellationToken cancellationToken = default);
    Task AddAsync(ManagementUser managementUser, CancellationToken cancellationToken = default);

    void Remove(ManagementUser managementUser);
}