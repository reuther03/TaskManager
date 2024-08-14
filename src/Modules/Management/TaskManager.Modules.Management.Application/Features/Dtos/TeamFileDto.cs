using TaskManager.Modules.Management.Domain.TeamFiles;

namespace TaskManager.Modules.Management.Application.Features.Dtos;

public class TeamFileDto
{
    public string FileUrl { get; init; }

    public static TeamFileDto AsDto(TeamFile teamFile)
    {
        return new TeamFileDto
        {
            FileUrl = teamFile.FileUrl
        };
    }
}