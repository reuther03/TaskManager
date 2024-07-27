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
        if (_taskItemIds.Contains(taskItem.Id))
        {
            throw new InvalidOperationException("Task is already added to the team");
        }

        _taskItemIds.Add(taskItem.Id);
    }
}