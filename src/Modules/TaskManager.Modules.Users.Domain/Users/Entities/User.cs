using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Modules.Users.Domain.Users.ValueObjects;

namespace TaskManager.Modules.Users.Domain.Users.Entities;

public class User : AggregateRoot<UserId>
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    //lista taskow przpisana z wszystkich grup
    //lista grup
}