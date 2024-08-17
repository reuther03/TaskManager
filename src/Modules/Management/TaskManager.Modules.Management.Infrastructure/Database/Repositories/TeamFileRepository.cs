using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.TeamFiles;

namespace TaskManager.Modules.Management.Infrastructure.Database.Repositories;

internal class TeamFileRepository : ITeamFileRepository
{
    private readonly DbSet<TeamFile> _teamFiles;

    public TeamFileRepository(ManagementsDbContext context)
    {
        _teamFiles = context.TeamFiles;
    }


    public Task<TeamFile?> GetByIdAsync(Guid fileId, CancellationToken cancellationToken = default)
        => _teamFiles.FirstOrDefaultAsync(x => x.Id == fileId, cancellationToken);

    public void Remove(TeamFile file)
        => _teamFiles.Remove(file);
}