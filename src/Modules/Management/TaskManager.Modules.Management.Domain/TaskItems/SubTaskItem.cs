using TaskManager.Abstractions.Exception;
using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;

namespace TaskManager.Modules.Management.Domain.TaskItems;

public class SubTaskItem : Entity<Guid>
{
    public Name TaskName { get; private set; }
    public Description Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime Deadline { get; private set; }
    public TaskProgress Progress { get; private set; }


    protected SubTaskItem()
    {
    }

    private SubTaskItem(Guid id, Name taskName, Description description, DateTime createdAt, DateTime deadline, TaskProgress progress) : base(id)
    {
        TaskName = taskName;
        Description = description;
        CreatedAt = createdAt;
        Deadline = deadline;
        Progress = progress;
    }

    public static SubTaskItem Create(Name taskName, Description description, DateTime deadline)
    {
        var subTask = new SubTaskItem(
            Guid.NewGuid(),
            taskName,
            description,
            DateTime.Now,
            deadline,
            TaskProgress.ToDo
        );

        if (deadline < DateTime.Now)
            throw new DomainException("Deadline must be in the future.");

        return subTask;
    }
}