using TaskManager.Abstractions.Exception;
using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Modules.Management.Domain.Groups.ValueObjects;

namespace TaskManager.Modules.Management.Domain.Groups.Entities;

public class GroupTask : Entity<Guid>
{
    public Name TaskName { get; private set; }
    public Description Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime Deadline { get; private set; }
    public bool Priority { get; private set; }
    public TaskProgress Progress { get; private set; }

    protected GroupTask()
    {
    }

    public GroupTask(Guid id, Name taskName, Description description, DateTime createdAt, DateTime deadline,
        bool priority, TaskProgress progress) : base(id)
    {
        TaskName = taskName;
        Description = description;
        CreatedAt = createdAt;
        Deadline = deadline;
        Priority = priority;
        Progress = progress;
    }

    public static GroupTask Create(Name taskName, Description description, DateTime deadline,
        bool priority)
    {
        var groupTask = new GroupTask(Guid.NewGuid(), taskName, description, DateTime.Now, deadline, priority, TaskProgress.DoTo);

        if (deadline < DateTime.Now)
        {
            throw new DomainException("Deadline must be in the future.");
        }

        return groupTask;
    }
}