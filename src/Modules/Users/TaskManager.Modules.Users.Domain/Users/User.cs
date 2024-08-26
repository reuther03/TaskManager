using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.Kernel.ValueObjects.User;

namespace TaskManager.Modules.Users.Domain.Users;

public class User : AggregateRoot<UserId>
{
    public Name FullName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }

    public string? ProfilePicturePublicId { get; private set; }
    public string? ProfilePicture { get; private set; }

    protected User()
    {
    }

    private User(UserId id, Name fullName, Email email, Password password) : base(id)
    {
        FullName = fullName;
        Email = email;
        Password = password;
        ProfilePicture = null;
    }

    public static User Create(string fullName, string email, string password)
        => new User(UserId.New(), fullName, email, password);
    public void Update(string fullName, string email, string profilePicture, string publicId)
    {
        FullName = fullName;
        Email = email;
        AddProfilePicture(profilePicture, publicId);
    }

    public void AddProfilePicture(string profilePicture, string publicId)
    {
        if (string.IsNullOrWhiteSpace(profilePicture) && profilePicture == ProfilePicture)
            throw new ArgumentException("Profile picture is invalid");

        ProfilePicturePublicId = publicId;
        ProfilePicture = profilePicture;
    }

}