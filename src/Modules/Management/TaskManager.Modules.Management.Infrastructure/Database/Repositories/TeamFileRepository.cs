using Microsoft.EntityFrameworkCore;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.TeamFiles;

namespace TaskManager.Modules.Management.Infrastructure.Database.Repositories;

internal class TeamFileRepository : ITeamFileRepository
{
    private readonly DbSet<TeamFile> _teamFiles;
    private readonly ManagementsDbContext _context;

    public TeamFileRepository(ManagementsDbContext context)
    {
        _context = context;
        _teamFiles = context.TeamFiles;
    }

    public Task<TeamFile?> GetByIdAsync(Guid fileId, CancellationToken cancellationToken = default)
        => _teamFiles.FirstOrDefaultAsync(x => x.Id == fileId, cancellationToken);

    // public Task<List<TeamFile>> GetAllByIdAsync(Guid teamId, CancellationToken cancellationToken = default)
    //     => _context.Teams
    //         .Include(x => x.TeamFiles)
    //         .Where(x => x.Id.Value == teamId)
    //         .SelectMany(x => x.TeamFiles)
    //         .ToListAsync(cancellationToken);

    public void Remove(TeamFile file)
        => _teamFiles.Remove(file);
}