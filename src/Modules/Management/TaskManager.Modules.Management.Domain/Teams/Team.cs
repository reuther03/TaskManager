using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Domain.TeamMembers;

namespace TaskManager.Modules.Management.Domain.Teams;

public class Team : AggregateRoot<TeamId>
{
    private readonly List<TaskItemId> _taskItemIds = [];
    // private readonly List<UserId> _userIds = [];
    private readonly List<TeamMember> _teamMembers = [];

    public Name Name { get; private set; }

    public IReadOnlyList<TaskItemId> TaskItemIds => _taskItemIds.AsReadOnly();
    // public IReadOnlyList<UserId> UserIds => _userIds.AsReadOnly();
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
}