using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Domain.Groups.ValueObjects;

namespace TaskManager.Modules.Management.Domain.Groups.Entities;

public class Team : AggregateRoot<TeamId>
{
    private readonly List<TeamTask> _tasks = [];
    private readonly List<UserId> _userIds = [];

    public Name Name { get; private set; }
    public IReadOnlyList<TeamTask> Tasks => _tasks.AsReadOnly();
    public IReadOnlyList<UserId> UserIds => _userIds.AsReadOnly();


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