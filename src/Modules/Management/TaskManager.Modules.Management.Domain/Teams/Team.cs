using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Domain.TeamMembers;

namespace TaskManager.Modules.Management.Domain.Teams;

public class Team : AggregateRoot<TeamId>
{
    private readonly List<TaskItemId> _taskItemIds = [];

    private readonly List<TeamMember> _teamMembers = [];

    public Name Name { get; private set; }
    public IReadOnlyList<TaskItemId> TaskItemIds => _taskItemIds.AsReadOnly();
    public IReadOnlyCollection<TeamMember> TeamMembers => _teamMembers.AsReadOnly();

    public int CompletedTasks { get; set; }
    public int TotalTasks => _taskItemIds.Count;

    public double Progress => TotalTasks == 0 ? 0 : (double)CompletedTasks / TotalTasks * 100;

    protected Team()
    {
    }

    public Team(TeamId id, Name name) : base(id)
    {
        Name = name;
    }

    public static Team Create(string teamName)
        => new Team(TeamId.New(), teamName);

    public void AddMember(TeamMember teamMember)
    {
        if (_teamMembers.Exists(x => x.UserId == teamMember.UserId))
        {
            throw new InvalidOperationException("User is already a member of the team");
        }

        _teamMembers.Add(teamMember);
    }

    public void AddTask(TaskItem taskItem)
    {
        if (_taskItemIds.Contains(taskItem.Id) || _taskItemIds.Count >= 100)
        {
            throw new InvalidOperationException("Task is already added or team has reached the maximum number of tasks");
        }

        _taskItemIds.Add(taskItem.Id);
    }

    public void IncrementCompletedTasks(TaskItemId taskItemId)
    {
        if (!_taskItemIds.Contains(taskItemId))
        {
            throw new InvalidOperationException("Task is not a member of the team");
        }

        CompletedTasks++;
    }
}