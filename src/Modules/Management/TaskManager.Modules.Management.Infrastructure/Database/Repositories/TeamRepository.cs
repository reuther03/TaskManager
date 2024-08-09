using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Infrastructure.Database.Repositories;

internal class TeamRepository : ITeamRepository
{
    private readonly ManagementsDbContext _context;
    private readonly DbSet<Team> _teams;

    public TeamRepository(ManagementsDbContext context)
    {
        _context = context;
        _teams = _context.Teams;
    }

    public Task<Team?> GetByIdAsync(TeamId id, CancellationToken cancellationToken = default)
        => _teams.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<int> GetCountedTeamMembersAsync(TeamId id, CancellationToken cancellationToken = default)
        => _teams.Where(x => x.Id == id).SelectMany(x => x.TeamMembers).CountAsync(cancellationToken);

    public Task GetTeamMembersAsync(TeamId id, CancellationToken cancellationToken = default)
        => _teams.Where(x => x.Id == id).Select(x => x.TeamMembers).ToListAsync(cancellationToken);

    public Task<bool> ExistsAsync(TeamId id, CancellationToken cancellationToken = default)
        => _teams.AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);

    public async Task AddAsync(Team team, CancellationToken cancellationToken = default)
        => await _teams.AddAsync(team, cancellationToken);
}