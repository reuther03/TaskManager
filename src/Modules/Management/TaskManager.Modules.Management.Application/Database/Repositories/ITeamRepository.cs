using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Database.Repositories;

public interface ITeamRepository
{
    Task<bool> ExistsAsync(TeamId id, CancellationToken cancellationToken = default);
    Task AddAsync(Team team, CancellationToken cancellationToken = default);
}