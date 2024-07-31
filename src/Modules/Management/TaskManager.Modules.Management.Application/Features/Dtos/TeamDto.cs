using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Features.Dtos;

public class TeamDto
{
    public Guid Id { get; init; } = Guid.Empty;
    public string Name { get; init; } = null!;
    public double TeamProgress { get; set; }

    public static TeamDto AsDto(Team team)
    {
        return new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            TeamProgress = team.Progress
        };
    }
}