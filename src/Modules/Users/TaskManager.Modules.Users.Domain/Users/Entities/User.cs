using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Users.Domain.Users.ValueObjects;

namespace TaskManager.Modules.Users.Domain.Users.Entities;

public class User : AggregateRoot<UserId>
{
    public Name FullName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public string ProfilePictureUrl { get; private set; }

    protected User()
    {
    }

    private User(UserId id, Name fullName, Email email, Password password, string profilePictureUrl) : base(id)
    {
        FullName = fullName;
        Email = email;
        Password = password;
        ProfilePictureUrl = profilePictureUrl;
    }

    public static User Create(string fullName, string email, string password, string profilePictureUrl)
        => new User(UserId.New(), fullName, email, password, profilePictureUrl);
}