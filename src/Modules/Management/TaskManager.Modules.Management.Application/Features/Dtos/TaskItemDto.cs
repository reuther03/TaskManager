using TaskManager.Modules.Management.Domain.TaskItems;

namespace TaskManager.Modules.Management.Application.Features.Dtos;

public class TaskItemDto
{
    public string TaskName { get; set; }
    public string Description { get; set; }
    public IList<SubTaskDto> SubTaskItems { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime Deadline { get; set; }
    public bool Priority { get; set; }
    public TaskProgress Progress { get; set; }
    public Guid? AssignedUserId { get; set; }

    public static TaskItemDto AsDto(TaskItem taskItem)
    {
        return new TaskItemDto
        {
            TaskName = taskItem.TaskName.Value,
            Description = taskItem.Description.Value,
            SubTaskItems = taskItem.SubTaskItems.Select(SubTaskDto.AsDto).ToList(),
            CreatedAt = taskItem.CreatedAt,
            Deadline = taskItem.Deadline,
            Priority = taskItem.Priority,
            Progress = taskItem.Progress
        };
    }
}