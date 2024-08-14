using TaskManager.Abstractions.Kernel.Primitives;

namespace TaskManager.Modules.Management.Domain.TeamFiles;

public class TeamFile : Entity<Guid>
{
    public string FileUrl { get; private set; }

    public TeamFile(Guid id, string fileUrl) : base(id)
    {
        FileUrl = fileUrl;
    }

    public static TeamFile Create(string fileUrl)
        => new TeamFile(Guid.NewGuid(), fileUrl);
}