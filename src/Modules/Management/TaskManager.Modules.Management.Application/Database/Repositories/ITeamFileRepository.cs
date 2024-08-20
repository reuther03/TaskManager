using TaskManager.Modules.Management.Domain.TeamFiles;

namespace TaskManager.Modules.Management.Application.Database.Repositories;

public interface ITeamFileRepository
{
    Task<TeamFile?> GetByIdAsync(Guid fileId, CancellationToken cancellationToken = default);
    // Task<List<TeamFile>> GetAllByIdAsync(Guid teamId, CancellationToken cancellationToken = default);
    void Remove(TeamFile file);
}