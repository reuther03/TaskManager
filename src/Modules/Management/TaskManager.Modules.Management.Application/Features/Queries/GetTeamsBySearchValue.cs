using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Kernel.Pagination;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Queries;
using TaskManager.Abstractions.QueriesAndCommands.TypeExtensions;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database.Abstractions;
using TaskManager.Modules.Management.Application.Features.Dtos;

namespace TaskManager.Modules.Management.Application.Features.Queries;

public record GetTeamsBySearchValue : IQuery<PaginatedList<TeamDto>>
{
    public string? SearchValue { get; init; } = string.Empty;
    public double? Progress { get; init; } = null;
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    internal sealed class Handler : IQueryHandler<GetTeamsBySearchValue, PaginatedList<TeamDto>>
    {
        private readonly IManagementsDbContext _dbContext;
        private readonly IUserService _userService;

        public Handler(IManagementsDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result<PaginatedList<TeamDto>>> Handle(GetTeamsBySearchValue request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == _userService.UserId, cancellationToken);
            if (user == null)
                return Result<PaginatedList<TeamDto>>.NotFound("User not found");

            var teams = await _dbContext.Teams
                .Where(x => x.TeamMembers.Any(y => y.UserId == user.Id))
                .WhereIf(request.Progress.HasValue, x => x.Progress >= request.Progress)
                .WhereIf(
                    !string.IsNullOrWhiteSpace(request.SearchValue),
                    x => EF.Functions.Like(x.Name, $"%{request.SearchValue}%"))
                .ToListAsync(cancellationToken);

            var totalTeams = await _dbContext.Teams.CountAsync(cancellationToken);
            var teamsDto = teams.Select(TeamDto.AsDto).ToList();

            return PaginatedList<TeamDto>.Create(request.Page, request.PageSize, totalTeams, teamsDto);
        }
    }
}