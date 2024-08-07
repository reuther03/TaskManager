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
    public double Progress { get; private set; }
    public IReadOnlyList<TaskItemId> TaskItemIds => _taskItemIds.AsReadOnly();
    public IReadOnlyCollection<TeamMember> TeamMembers => _teamMembers.AsReadOnly();

    protected Team()
    {
    }

    public Team(TeamId id, Name name) : base(id)
    {
        Name = name;
        Progress = 0;
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

    public void SetProgress(double progress)
    {
        if (progress is < 0 or > 100)
        {
            throw new InvalidOperationException("Progress must be between 0 and 100");
        }

        Progress = progress;
    }
}