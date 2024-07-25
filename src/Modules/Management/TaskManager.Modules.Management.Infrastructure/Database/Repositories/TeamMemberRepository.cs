using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.TeamMembers;

namespace TaskManager.Modules.Management.Infrastructure.Database.Repositories;

internal class TeamMemberRepository : ITeamMemberRepository
{
    private readonly ManagementsDbContext _context;
    private readonly DbSet<TeamMember> _teamMembers;

    public TeamMemberRepository(ManagementsDbContext context)
    {
        _context = context;
        _teamMembers = _context.TeamMembers;
    }

    public async Task<TeamMember> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default)
        => await _teamMembers.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
}