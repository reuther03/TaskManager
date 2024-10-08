﻿using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.TeamMembers;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Infrastructure.Database.Repositories;

internal class TeamMemberRepository : ITeamMemberRepository
{
    private readonly DbSet<TeamMember> _teamMembers;

    public TeamMemberRepository(ManagementsDbContext context)
    {
        _teamMembers = context.TeamMembers;
    }

    public async Task<TeamMember?> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default)
        => await _teamMembers.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

    public Task<List<TeamMember>> GetAllByIdAsync(UserId userId, CancellationToken cancellationToken = default)
        => _teamMembers.Where(x => x.UserId == userId).ToListAsync(cancellationToken);

    public async Task<List<TeamMember>> GetTeamMembersAsync(TeamId id, CancellationToken cancellationToken = default)
        => await _teamMembers.Where(x => x.TeamId == id).ToListAsync(cancellationToken);

    public Task<bool> InSameTeamAsync(UserId userId1, UserId userId2, TeamId teamId, CancellationToken cancellationToken = default)
    => _teamMembers.Select(x => x.UserId == userId1 && x.UserId == userId2 && x.TeamId == teamId).AnyAsync(cancellationToken);

    public Task<bool> MemberInTeamAsync(UserId userId, TeamId teamId, CancellationToken cancellationToken = default)
        => _teamMembers.Select(x => x.UserId == userId && x.TeamId == teamId).AnyAsync(cancellationToken);
}