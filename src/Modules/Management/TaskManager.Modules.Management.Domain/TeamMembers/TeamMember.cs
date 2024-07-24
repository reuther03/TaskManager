using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Domain.TeamMembers;

public class TeamMember : Entity<Guid>
{
    public UserId UserId { get; private set; }
    public TeamId TeamId { get; private set; }
    public TeamRole TeamRole { get; private set; }

    protected TeamMember()
    {
    }

    public TeamMember(Guid id, UserId userId, TeamId teamId, TeamRole teamRole) : base(id)
    {
        UserId = userId;
        TeamId = teamId;
        TeamRole = teamRole;
    }

    public static TeamMember Create(UserId userId, TeamId teamId, TeamRole teamRole)
        => new TeamMember(Guid.NewGuid(), userId, teamId, teamRole);
}