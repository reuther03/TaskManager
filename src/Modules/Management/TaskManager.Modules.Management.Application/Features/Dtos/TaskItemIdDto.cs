using TaskManager.Abstractions.Kernel.ValueObjects.User;
using TaskManager.Modules.Management.Domain.TaskItems;

namespace TaskManager.Modules.Management.Application.Features.Dtos;

public class TaskItemIdDto
{
    public Guid TaskId { get; init; }
    public List<SubTaskDto> SubTasks { get; init; } = null!;

    public static TaskItemIdDto AsDto(TaskItem taskItem)
    {
        return new TaskItemIdDto
        {
            TaskId = taskItem.Id,
            SubTasks = taskItem.SubTaskItems.Select(subTaskItem => SubTaskDto.AsDto((SubTaskItem)subTaskItem)).ToList()

        };
    }
}