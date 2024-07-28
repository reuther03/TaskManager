using TaskManager.Abstractions.Exception;
using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Abstractions.Kernel.ValueObjects.User;

namespace TaskManager.Modules.Management.Domain.TaskItems;

public class TaskItem : AggregateRoot<TaskItemId>
{
    public Name TaskName { get; private set; }
    public Description Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime Deadline { get; private set; }
    public bool Priority { get; private set; }
    public TaskProgress Progress { get; private set; }
    public UserId? AssignedUserId { get; set; }

    protected TaskItem()
    {
    }

    private TaskItem(Guid id, Name taskName, Description description, DateTime createdAt, DateTime deadline,
        bool priority, TaskProgress progress, Guid? assignerUserId) : base(id)
    {
        TaskName = taskName;
        Description = description;
        CreatedAt = createdAt;
        Deadline = deadline;
        Priority = priority;
        Progress = progress;
        AssignedUserId = assignerUserId;
    }

    public static TaskItem Create(Name taskName, Description description, DateTime deadline,
        bool priority, Guid? assignerUserId)
    {
        var groupTask = new TaskItem(
            Guid.NewGuid(),
            taskName,
            description,
            DateTime.Now,
            deadline,
            priority,
            TaskProgress.ToDo,
            assignerUserId
        );

        if (deadline < DateTime.Now)
            throw new DomainException("Deadline must be in the future.");

        return groupTask;
    }

    public void ChangeStatus(TaskProgress progress)
    {
        if (Progress == TaskProgress.Completed)
            throw new DomainException("Task is already done.");

        Progress = progress;
    }
}