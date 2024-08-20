using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Domain.TeamFiles;
using TaskManager.Modules.Management.Domain.TeamMembers;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Database.Repositories;

public interface ITeamRepository
{
    Task<Team?> GetByIdAsync(TeamId id, CancellationToken cancellationToken = default);
    Task<int> GetCountedTeamMembersAsync(TeamId id, CancellationToken cancellationToken = default);
    Task<bool> TaskInTeamAsync(TeamId teamId, TaskItemId taskId, CancellationToken cancellationToken = default);
    Task AddAsync(Team team, CancellationToken cancellationToken = default);
    IList<Team> GetTeamsByUserIdAsync(TeamMember teamMember, CancellationToken cancellationToken = default);
    Task<List<TeamFile>> GetTeamFilesAsync(TeamId teamId, CancellationToken cancellationToken = default);
    void Remove(Team team);
}