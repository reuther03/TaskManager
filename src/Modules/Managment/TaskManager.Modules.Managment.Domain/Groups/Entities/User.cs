using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;

namespace TaskManager.Modules.Managment.Domain.Groups.Entities;

public class User : Entity<Guid>
{
    public Name FullName { get; private set; }
    public Email Email { get; private set; }

    public User(Guid id, Name fullName, Email email) : base(id)
    {
        FullName = fullName;
        Email = email;
    }
}