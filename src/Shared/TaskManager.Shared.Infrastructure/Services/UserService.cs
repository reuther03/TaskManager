using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Abstractions.Services;
using TaskManager.Infrastructure.Auth;

namespace TaskManager.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    public UserId? UserId => IsAuthenticated ? GetUserIdFromClaims(_httpContextAccessor.HttpContext?.User) : null;
    public Email? Email => IsAuthenticated ? GetEmailFromClaims(_httpContextAccessor.HttpContext?.User) : null;

    private static UserId? GetUserIdFromClaims(ClaimsPrincipal? claims)
    {
        if (claims is null)
            return null;

        var userId = claims.FindFirst(ClaimTypes.Name)?.Value;
        return userId is null ? null : UserId.From(userId);
    }

    private static Email? GetEmailFromClaims(ClaimsPrincipal? claims)
    {
        if (claims is null)
            return null;

        var email = claims.FindFirst(ClaimConsts.Email)?.Value;
        return email is null ? null : new Email(email);
    }
}