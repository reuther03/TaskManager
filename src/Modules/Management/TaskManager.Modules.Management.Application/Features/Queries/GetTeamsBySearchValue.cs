using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskManager.Abstractions.Kernel.Pagination;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Queries;
using TaskManager.Abstractions.QueriesAndCommands.TypeExtensions;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database.Abstractions;
using TaskManager.Modules.Management.Application.Features.Dtos;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Features.Queries;

public record GetTeamsBySearchValue(string SearchValue, int Page = 1, int PageSize = 10) : IQuery<PaginatedList<TeamDto>>
{
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
            {
                return Result<PaginatedList<TeamDto>>.NotFound("User not found");
            }

            var teams = await _dbContext.Teams
                .Where(t => t.TeamMembers.Any(member => member.UserId == user.Id))
                .ToListAsync(cancellationToken);

            var filteredTeams = teams.AsQueryable()
                .FilterByProperty(t => t.Name.Value, request.SearchValue)
                .AsEnumerable();

            var teamDtos = filteredTeams
                .Select(TeamDto.AsDto)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return Result.Ok(new PaginatedList<TeamDto>(request.Page, request.PageSize, teams.Count, teamDtos));
        }
    }
}