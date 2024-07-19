using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Users.Application.Abstractions.Database.Repositories;
using TaskManager.Modules.Users.Domain.Users.Entities;

namespace TaskManager.Modules.Users.Infrastructure.Database.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly UsersDbContext _context;

    public UserRepository(UsersDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsWithEmailAsync(string email, CancellationToken cancellationToken = default)
        => _context.Users.AnyAsync(x => x.Email == email, cancellationToken);

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.Users.AnyAsync(x => x.Id.Equals(id), cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        => await _context.AddAsync(user, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}