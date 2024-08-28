using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Kernel.Pagination;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Queries;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database.Abstractions;
using TaskManager.Modules.Management.Application.Features.Dtos;
using TaskManager.Modules.Management.Domain.TaskItems;

namespace TaskManager.Modules.Management.Application.Features.Queries;

public record GetTeamTasksByStatusQuery(
    [property: JsonIgnore]
    Guid TeamId,
    TaskProgress TaskProgress,
    int Page = 1,
    int PageSize = 10) : IQuery<PaginatedList<TaskItemDto>>
{
    internal sealed class Handler : IQueryHandler<GetTeamTasksByStatusQuery, PaginatedList<TaskItemDto>>
    {
        private readonly IManagementsDbContext _dbContext;
        private readonly IUserService _userService;

        public Handler(IManagementsDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Result<PaginatedList<TaskItemDto>>> Handle(GetTeamTasksByStatusQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == _userService.UserId, cancellationToken);
            if (user == null)
                return Result<PaginatedList<TaskItemDto>>.NotFound("User not found");

            var team = await _dbContext.Teams.FirstOrDefaultAsync(x => x.Id == Domain.Teams.TeamId.From(request.TeamId), cancellationToken);
            if (team == null)
                return Result<PaginatedList<TaskItemDto>>.NotFound("Team not found");

            var taskItems = await _dbContext.Tasks
                .Where(t => team.TaskItemIds.Contains(t.Id) && t.Progress == request.TaskProgress)
                .ToListAsync(cancellationToken);

            var taskItemDtos = taskItems.Select(TaskItemDto.AsDto).ToList();

            return PaginatedList<TaskItemDto>.Create(request.Page, request.PageSize, taskItemDtos.Count, taskItemDtos);
        }
    }
}