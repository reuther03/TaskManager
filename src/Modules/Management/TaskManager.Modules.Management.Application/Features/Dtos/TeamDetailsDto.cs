using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Features.Dtos;

public class TeamDetailsDto
{
    public string Name { get; init; }
    public List<TaskItemIdDto> TaskItems { get; init; }
    public List<TeamMemberIdDto> TeamMembers { get; init; }

    public TeamDetailsDto(string name, List<TaskItemIdDto> taskItems, List<TeamMemberIdDto> teamMembers)
    {
        Name = name;
        TaskItems = taskItems;
        TeamMembers = teamMembers;
    }
}