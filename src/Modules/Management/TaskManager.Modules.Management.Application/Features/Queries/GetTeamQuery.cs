using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Queries;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database.Abstractions;
using TaskManager.Modules.Management.Application.Features.Dtos;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Features.Queries;

public record GetTeamQuery(Guid CurrentTeamId) : IQuery<TeamDto>
{
    internal sealed class Handler : IQueryHandler<GetTeamQuery, TeamDto>
    {
        private readonly IManagementsDbContext _dbContext;
        private readonly IUserService _userService;

        public Handler(IManagementsDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result<TeamDto>> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.TeamMembers
                .FirstOrDefaultAsync(x => x.UserId == _userService.UserId, cancellationToken);

            if (user is null)
                return Result.Unauthorized<TeamDto>("User not found");

            if (user.TeamId != TeamId.From(request.CurrentTeamId))
                return Result.BadRequest<TeamDto>("User is not a member of the team");

            var team = await _dbContext.Teams
                .Include(x => x.TeamMembers)
                .Include(x => x.TaskItemIds)
                .FirstOrDefaultAsync(x => x.Id == TeamId.From(request.CurrentTeamId), cancellationToken);

            return team is null
                ? Result.NotFound<TeamDto>("Team not found")
                : Result.Ok(TeamDto.AsDto(team));
        }
    }
}