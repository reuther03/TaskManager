using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Users.Application.Abstractions.Database.Repositories;
using TaskManager.Modules.Users.Domain.Users;

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

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
     => await _context.Users.FirstOrDefaultAsync(x => x.Id == UserId.From(id), cancellationToken);

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        => await _context.AddAsync(user, cancellationToken);

}