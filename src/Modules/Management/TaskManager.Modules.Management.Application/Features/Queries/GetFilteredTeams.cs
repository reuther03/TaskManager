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

public record GetFilteredTeams(string SearchValue, int Page = 1, int PageSize = 10) : IQuery<PaginatedList<TeamDto>>
{
    internal sealed class Handler : IQueryHandler<GetFilteredTeams, PaginatedList<TeamDto>>
    {
        private readonly IManagementsDbContext _dbContext;
        private readonly IUserService _userService;

        public Handler(IManagementsDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result<PaginatedList<TeamDto>>> Handle(GetFilteredTeams request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == _userService.UserId, cancellationToken);
            if (user == null)
            {
                return Result<PaginatedList<TeamDto>>.NotFound("User not found");
            }

            // Retrieve teams where the current user is a member.
            var teams = await _dbContext.Teams
                .Where(t => t.TeamMembers.Any(member => member.UserId == user.Id))
                .ToListAsync(cancellationToken);

            // Convert the results back to IQueryable to use the extension method
            // Perform filtering in memory using AsEnumerable to bypass EF Core translation limitations.
            var filteredTeams = teams.AsQueryable()
                .FilterByProperty(t => t.Name.Value, request.SearchValue)
                .AsEnumerable();

            // Map the data to DTOs and paginate the results manually since in-memory.
            var teamDtos = filteredTeams
                .Select(t => TeamDto.AsDto(t))
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            // Return the final paginated result.
            return Result.Ok(new PaginatedList<TeamDto>(request.Page, request.PageSize, teams.Count, teamDtos));
        }
    }
}