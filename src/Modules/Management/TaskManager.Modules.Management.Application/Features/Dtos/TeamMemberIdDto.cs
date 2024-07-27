using TaskManager.Modules.Management.Domain.TeamMembers;

namespace TaskManager.Modules.Management.Application.Features.Dtos;

public class TeamMemberIdDto
{
    public Guid UserId { get; init; }

    public static TeamMemberIdDto AsDto(TeamMember teamMember)
    {
        return new TeamMemberIdDto
        {
            UserId = teamMember.UserId,
        };
    }
}