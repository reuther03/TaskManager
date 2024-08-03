using TaskManager.Modules.Users.Domain.Users;

namespace TaskManager.Modules.Users.Application.Users.Dto;

public class UserDto
{
    public string FullName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string ProfilePicture { get; init; } = null!;

    public static UserDto AsDto(User user)
    {
        return new UserDto
        {
            FullName = user.FullName,
            Email = user.Email,
            ProfilePicture = user.ProfilePicture ?? string.Empty
        };
    }
}