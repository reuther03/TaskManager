using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Features.Dtos;

public class TeamDetailsDto
{
    public string Name { get; init; } = null!;
    public List<TaskItemId> TaskItems { get; init; } = null!;
    public List<TeamMemberIdDto> TeamMembers { get; init; } = null!;

    public static TeamDetailsDto AsDto(Team team)
    {
        return new TeamDetailsDto
        {
            Name = team.Name,
            TaskItems = team.TaskItemIds.ToList(),
            TeamMembers = team.TeamMembers.Select(TeamMemberIdDto.AsDto).ToList()
        };
    }
}