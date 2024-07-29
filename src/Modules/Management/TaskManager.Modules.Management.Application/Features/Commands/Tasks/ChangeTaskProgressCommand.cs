using System.Text.Json.Serialization;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Domain.TeamMembers;

namespace TaskManager.Modules.Management.Application.Features.Commands.Tasks;

public record ChangeTaskStatusCommand(
    [property: JsonIgnore]
    Guid CurrentTeamId,
    Guid TaskId,
    TaskProgress TaskProgress) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<ChangeTaskStatusCommand, Guid>
    {
        private readonly IUserService _userService;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamMemberRepository _memberRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserService userService, ITeamRepository teamRepository, ITeamMemberRepository memberRepository, ITaskRepository taskRepository,
            IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _teamRepository = teamRepository;
            _memberRepository = memberRepository;
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _memberRepository.GetByIdAsync(_userService.UserId, cancellationToken);
            if (user is null)
                return Result<Guid>.Unauthorized("User not found");

            var team = await _teamRepository.GetByIdAsync(request.CurrentTeamId, cancellationToken);
            if (team is null)
                return Result<Guid>.NotFound("Team not found");

            var task = await _taskRepository.GetByIdAsync(TaskItemId.From(request.TaskId), cancellationToken);
            if (task is null)
                return Result<Guid>.NotFound("Task not found");

            if (team.TaskItemIds.FirstOrDefault(x => x == TaskItemId.From(request.TaskId)) is null)
                return Result<Guid>.NotFound("Task not found in team");

            if (task.AssignedUserId != user.UserId && user.TeamRole is not (TeamRole.Leader or TeamRole.Admin))
                return Result<Guid>.BadRequest("Task is not assigned to you");


            task.ChangeStatus(request.TaskProgress);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok(task.Id.Value);
        }
    }
}