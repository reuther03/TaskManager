using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Domain.TeamMembers;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Features.Commands.Teams;

public record AddTaskCommand(
    Guid CurrentTeamId,
    string Name,
    string Description,
    DateTime Deadline,
    bool Priority,
    Guid? AssignedUserId) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<AddTaskCommand, Guid>
    {
        private readonly ITeamMemberRepository _memberRepository;
        private readonly IUserService _userService;
        private readonly ITaskRepository _taskRepository;
        private readonly ITeamRepository _teamRepository;

        public Handler(
            ITeamMemberRepository memberRepository,
            IUserService userService,
            ITaskRepository taskRepository,
            ITeamRepository teamRepository)
        {
            _memberRepository = memberRepository;
            _userService = userService;
            _taskRepository = taskRepository;
            _teamRepository = teamRepository;
        }

        public async Task<Result<Guid>> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            if (!_userService.IsAuthenticated)
                return Result<Guid>.BadRequest("User is not authenticated");

            var currentUser = await _memberRepository.GetByIdAsync(_userService.UserId, cancellationToken);

            if (currentUser.TeamId != TeamId.From(request.CurrentTeamId) && currentUser.TeamRole is not (TeamRole.Admin or TeamRole.Leader))
                return Result<Guid>.BadRequest("User is not a member of this team or does not have the necessary permissions");

            var team = await _teamRepository.GetByIdAsync(TeamId.From(request.CurrentTeamId), cancellationToken);
            if (team is null)
                return Result<Guid>.NotFound("Team not found");


            //todo:sprobowac zrobic zeby user nie musial podawac swojego id i zeby samo sie pobieralo

            var assignedUserId = request.AssignedUserId;
            if (assignedUserId is null || team.TeamMembers.Count == 1)
            {
                assignedUserId = currentUser.UserId;
            }

            var task = TaskItem.Create(
                new Name(request.Name),
                new Description(request.Description),
                request.Deadline,
                request.Priority,
                assignedUserId
            );

            team.AddTask(task);

            await _taskRepository.AddAsync(task, cancellationToken);
            await _teamRepository.UpdateAsync(team, cancellationToken);

            return Result.Ok(task.Id.Value);
        }
    }
}