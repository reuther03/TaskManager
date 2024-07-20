using TaskManager.Modules.Users.Domain.Users.Entities;

namespace TaskManager.Modules.Users.Application.Abstractions.Database.Repositories;

public interface IUserRepository
{
    Task<bool> ExistsWithEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}