using TaskManager.Modules.Management.Domain.TaskItems;

namespace TaskManager.Modules.Management.Application.Features.Dtos;

public class SubTaskDto
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public DateTime Deadline { get; init; }
    public TaskProgress Progress { get; init; }

    public static SubTaskDto AsDto(SubTaskItem subTask)
    {
        return new SubTaskDto
        {
            Name = subTask.TaskName,
            Description = subTask.Description,
            Deadline = subTask.Deadline,
            Progress = subTask.Progress
        };
    }
}