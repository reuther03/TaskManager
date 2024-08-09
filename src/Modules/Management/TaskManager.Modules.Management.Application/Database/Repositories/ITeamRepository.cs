using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Database.Repositories;

public interface ITeamRepository
{
    Task<Team?> GetByIdAsync(TeamId id, CancellationToken cancellationToken = default);
    Task<int> GetCountedTeamMembersAsync(TeamId id, CancellationToken cancellationToken = default);
    public Task GetTeamMembersAsync(TeamId id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(TeamId id, CancellationToken cancellationToken = default);
    Task AddAsync(Team team, CancellationToken cancellationToken = default);
}