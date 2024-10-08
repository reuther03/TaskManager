﻿using System.Text.Json.Serialization;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Abstractions.Services;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Application.Workflows;
using TaskManager.Modules.Management.Domain.TaskItems;
using TaskManager.Modules.Management.Domain.TeamMembers;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Application.Features.Commands.Teams;

public record AddTaskCommand(
    [property: JsonIgnore]
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
        private readonly IWorkflowEngine _workflowEngine;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(
            ITeamMemberRepository memberRepository,
            IUserService userService,
            ITaskRepository taskRepository,
            ITeamRepository teamRepository,
            IWorkflowEngine workflowEngine,
            IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _userService = userService;
            _taskRepository = taskRepository;
            _teamRepository = teamRepository;
            _workflowEngine = workflowEngine;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _memberRepository.GetByIdAsync(_userService.UserId, cancellationToken);

            if (currentUser.TeamId != TeamId.From(request.CurrentTeamId) && currentUser.TeamRole is not (TeamRole.Admin or TeamRole.Leader))
                return Result<Guid>.BadRequest("User is not a member of this team or does not have the necessary permissions");

            var team = await _teamRepository.GetByIdAsync(TeamId.From(request.CurrentTeamId), cancellationToken);
            if (team is null)
                return Result<Guid>.NotFound("Team not found");

            var teamMembers = await _teamRepository.GetCountedTeamMembersAsync(team.Id, cancellationToken);

            var assignedUserId = request.AssignedUserId;
            if (teamMembers == 1)
            {
                assignedUserId = currentUser.UserId;
            }

            assignedUserId ??= await _workflowEngine.AssignMemberToTaskAsync(request.CurrentTeamId, cancellationToken);

            var task = TaskItem.Create(
                new Name(request.Name),
                new Description(request.Description),
                request.Deadline,
                request.Priority,
                assignedUserId
            );

            team.AddTask(task);

            await _taskRepository.AddAsync(task, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok(task.Id.Value);
        }
    }
}