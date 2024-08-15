using TaskManager.Abstractions.Kernel.Primitives;

namespace TaskManager.Modules.Management.Domain.TeamFiles;

public class TeamFile : Entity<Guid>
{
    public string FileUrl { get; private set; }
    public string FileName { get; private set; }

    private TeamFile()
    {
    }

    public TeamFile(Guid id, string fileUrl, string fileName) : base(id)
    {
        FileUrl = fileUrl;
        FileName = fileName;
    }

    public static TeamFile Create(string fileUrl, string fileName)
        => new TeamFile(Guid.NewGuid(), fileUrl, fileName);
}