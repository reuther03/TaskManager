using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Domain.TeamFiles;
using TaskManager.Modules.Management.Domain.TeamMembers;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Infrastructure.Database.Repositories;

internal class TeamRepository : ITeamRepository
{
    private readonly DbSet<Team> _teams;

    public TeamRepository(ManagementsDbContext context)
    {
        _teams = context.Teams;
    }

    public Task<Team?> GetByIdAsync(TeamId id, CancellationToken cancellationToken = default)
        => _teams
            .Include(x => x.TeamFiles)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<int> GetCountedTeamMembersAsync(TeamId id, CancellationToken cancellationToken = default)
        => _teams.Where(x => x.Id == id).SelectMany(x => x.TeamMembers).CountAsync(cancellationToken);

    public Task<bool> TaskInTeamAsync(TeamId teamId, TaskItemId taskId, CancellationToken cancellationToken = default)
        => _teams.Where(x => x.TaskItemIds.Contains(taskId)).AnyAsync(x => x.Id == teamId, cancellationToken);

    public async Task AddAsync(Team team, CancellationToken cancellationToken = default)
        => await _teams.AddAsync(team, cancellationToken);

    public IList<Team> GetTeamsByUserIdAsync(TeamMember teamMember, CancellationToken cancellationToken = default)
        => _teams
            .Include(x => x.TeamMembers)
            .Where(x => x.TeamMembers.Any(y => y.UserId == teamMember.UserId)).ToList();

    public Task<List<TeamFile>> GetTeamFilesAsync(TeamId teamId, CancellationToken cancellationToken = default)
        => _teams
            .Include(x => x.TeamFiles)
            .Where(x => x.Id == teamId)
            .SelectMany(x => x.TeamFiles)
            .ToListAsync(cancellationToken);

    public void Remove(Team team)
        => _teams.Remove(team);
}