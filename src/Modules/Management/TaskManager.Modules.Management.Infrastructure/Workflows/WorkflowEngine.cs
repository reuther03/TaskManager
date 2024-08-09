using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Management.Application.Database.Abstractions;
using TaskManager.Modules.Management.Application.Workflows;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Infrastructure.Workflows;

public class WorkflowEngine : IWorkflowEngine
{
    private readonly IManagementsDbContext _dbContext;

    public WorkflowEngine(IManagementsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AssignMemberToTaskAsync(Guid teamId, CancellationToken cancellationToken = default)
    {
        var team = await _dbContext.Teams.FirstOrDefaultAsync(x => x.Id == TeamId.From(teamId), cancellationToken);
        if (team == null)
            throw new ArgumentException("Team not found");

        var tasks = await _dbContext.Tasks
            .Where(x => team.TaskItemIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var teamMembers = await _dbContext.TeamMembers
            .Where(x => x.TeamId == team.Id).ToListAsync(cancellationToken);

        var assignmentCounts = teamMembers
            .ToDictionary(
                member => member.UserId,
                member => tasks.Count(task => task.AssignedUserId == member.UserId)
            );

        var memberWithLeastTasks = assignmentCounts.OrderBy(x => x.Value).FirstOrDefault();
        if (memberWithLeastTasks.Key == null)
            throw new ArgumentException("No members found");

        return memberWithLeastTasks.Key;
    }
}