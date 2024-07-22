using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.Kernel.ValueObjects.User;

namespace TaskManager.Modules.Management.Domain.Groups.Entities;

public class GroupUser : AggregateRoot<UserId>
{
    public Name FullName { get; private set; }
    public Email Email { get; private set; }

    protected GroupUser()
    {
    }

    public GroupUser(Guid id, Name fullName, Email email) : base(id)
    {
        FullName = fullName;
        Email = email;
    }
}