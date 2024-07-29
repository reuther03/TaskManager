using System.Diagnostics.CodeAnalysis;
using TaskManager.Abstractions.Kernel.ValueObjects.User;

namespace TaskManager.Abstractions.Services;

public interface IUserService
{
    [MemberNotNullWhen(true, nameof(UserId),nameof(Email))]
    public bool IsAuthenticated { get; }
    public UserId? UserId { get; }
    public Kernel.ValueObjects.User.Email? Email { get; }
}