using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.Kernel.ValueObjects.User;

namespace TaskManager.Modules.Management.Domain.Groups.Entities;

public class TeamUser : AggregateRoot<UserId>
{
    public Name FullName { get; private set; }
    public Email Email { get; private set; }

    protected TeamUser()
    {
    }

    public TeamUser(Guid id, Name fullName, Email email) : base(id)
    {
        FullName = fullName;
        Email = email;
    }
}