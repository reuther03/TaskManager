using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Modules.Management.Domain.Groups.ValueObjects;

namespace TaskManager.Modules.Management.Domain.Groups.Entities;

public class Group : AggregateRoot<GroupId>
{
    public Name GroupName { get; private set; }

    protected Group()
    {
    }

    public Group(GroupId id, Name groupName) : base(id)
    {
        GroupName = groupName;
    }

    public static Group Create(string groupName)
        => new Group(GroupId.New(), groupName);
}