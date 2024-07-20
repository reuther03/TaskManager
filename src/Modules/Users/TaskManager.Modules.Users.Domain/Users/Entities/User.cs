using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Modules.Users.Domain.Users.ValueObjects;

namespace TaskManager.Modules.Users.Domain.Users.Entities;

public class User : AggregateRoot<UserId>
{
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }

    //lista taskow przpisana z wszystkich grup
    //lista grup
    protected User()
    {
    }

    private User(UserId id, FullName fullName, Email email, Password password) : base(id)
    {
        FullName = fullName;
        Email = email;
        Password = password;
    }

    public static User Create(string fullName, string email, string password)
        => new User(UserId.New(), fullName, email, password);
}