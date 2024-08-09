using System.Text.Json.Serialization;
using TaskManager.Abstractions.Kernel.Primitives.Result;
using TaskManager.Abstractions.QueriesAndCommands.Commands;
using TaskManager.Modules.Management.Application.Database;
using TaskManager.Modules.Management.Application.Database.Repositories;
using TaskManager.Modules.Management.Domain.TaskItems;

namespace TaskManager.Modules.Management.Application.Features.Commands.Tasks;

public record AddSubTaskCommand(
    [property: JsonIgnore]
    Guid TeamId,
    [property: JsonIgnore]
    Guid TaskId,
    string Name,
    string Description
) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<AddSubTaskCommand, Guid>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITaskRepository taskRepository, ITeamRepository teamRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddSubTaskCommand request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByIdAsync(request.TeamId, cancellationToken);
            if (team is null)
                return Result<Guid>.BadRequest("Team not found");

            var task = await _taskRepository.GetByIdAsync(request.TaskId, cancellationToken);
            if (task is null || team.TaskItemIds.All(x => x != task.Id))
                return Result<Guid>.BadRequest("Task not found");

            var subTask = SubTaskItem.Create(
                request.Name,
                request.Description,
                task.Deadline
            );

            task.AddSubTaskItem(subTask);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result<Guid>.Ok(subTask.Id);
        }
    }
}