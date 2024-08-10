using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Kernel.Pagination;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Queries;
using TaskManager.Abstractions.QueriesAndCommands.TypeExtensions;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database.Abstractions;
using TaskManager.Modules.Management.Application.Features.Dtos;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Features.Queries;

public record GetMemberTeamsQuery(int Page = 1, int PageSize = 10) : IQuery<PaginatedList<TeamDto>>
{
    internal sealed class Handler : IQueryHandler<GetMemberTeamsQuery, PaginatedList<TeamDto>>
    {
        private readonly IManagementsDbContext _dbContext;
        private readonly IUserService _userService;

        public Handler(IManagementsDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result<PaginatedList<TeamDto>>> Handle(GetMemberTeamsQuery query, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == _userService.UserId, cancellationToken);
            if (user == null)
                return Result<PaginatedList<TeamDto>>.NotFound("User not found");

            var teams = await _dbContext.Teams
                .Where(x => x.TeamMembers.Any(z => z.UserId == user.Id))
                .ToPagedListAsync<Team, TeamDto>(query.Page, query.PageSize, x => TeamDto.AsDto(x), cancellationToken);

            return Result.Ok(teams);
        }
    }
}