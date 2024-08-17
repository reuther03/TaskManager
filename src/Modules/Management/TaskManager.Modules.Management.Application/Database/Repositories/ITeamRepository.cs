using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Database.Repositories;

public interface ITeamRepository
{
    Task<Team?> GetByIdAsync(TeamId id, CancellationToken cancellationToken = default);
    Task<int> GetCountedTeamMembersAsync(TeamId id, CancellationToken cancellationToken = default);
    Task<bool> TaskInTeamAsync(TeamId teamId, TaskItemId taskId, CancellationToken cancellationToken = default);
    Task AddAsync(Team team, CancellationToken cancellationToken = default);
    IList<Team> GetTeamsByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);
    void Remove(Team team);
}