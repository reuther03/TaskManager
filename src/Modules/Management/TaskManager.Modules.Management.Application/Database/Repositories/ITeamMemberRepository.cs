using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Domain.TeamMembers;

namespace TaskManager.Modules.Management.Application.Database.Repositories;

public interface ITeamMemberRepository
{
    Task<TeamMember> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default);
}