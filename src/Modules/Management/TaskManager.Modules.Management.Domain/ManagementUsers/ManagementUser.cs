using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.Kernel.ValueObjects.User;

namespace TaskManager.Modules.Management.Domain.ManagementUsers;

public class ManagementUser : AggregateRoot<UserId>
{
    public Name FullName { get; private set; }
    public Email Email { get; private set; }

    protected ManagementUser()
    {
    }

    public ManagementUser(Guid id, Name fullName, Email email) : base(id)
    {
        FullName = fullName;
        Email = email;
    }

    public static ManagementUser Create(UserId userId, Name fullName, Email email)
        => new ManagementUser(userId, fullName, email);
}