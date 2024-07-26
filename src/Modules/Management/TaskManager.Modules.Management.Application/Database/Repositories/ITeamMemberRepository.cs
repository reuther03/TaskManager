using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Domain.TeamMembers;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Database.Repositories;

public interface ITeamMemberRepository
{
    Task<TeamMember> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default);
    Task<List<TeamMember>> GetTeamMembersAsync(TeamId id, CancellationToken cancellationToken = default);
}