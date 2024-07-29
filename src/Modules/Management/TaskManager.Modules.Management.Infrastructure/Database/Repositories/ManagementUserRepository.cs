using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.ManagementUsers;

namespace TaskManager.Modules.Management.Infrastructure.Database.Repositories;

internal class ManagementUserRepository : IManagementUserRepository
{
    private readonly ManagementsDbContext _context;
    private readonly DbSet<ManagementUser> _users;

    public ManagementUserRepository(ManagementsDbContext context)
    {
        _context = context;
        _users = _context.Users;
    }

    public Task<bool> ExistsAsync(UserId id, CancellationToken cancellationToken = default)
        => _users.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);

    public async Task AddAsync(ManagementUser managementUser, CancellationToken cancellationToken = default)
        => await _users.AddAsync(managementUser, cancellationToken);

    public async Task<ManagementUser> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
        => await _users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}