using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Modules.Management.Domain.Teams;

namespace TaskManager.Modules.Management.Domain.TaskItems;

public record TaskItemId : EntityId
{
    public TaskItemId(Guid value) : base(value)
    {
    }

    public static TaskItemId New() => new(Guid.NewGuid());
    public static TaskItemId From(Guid value) => new(value);
    public static TaskItemId From(string value) => new(Guid.Parse(value));

    public static implicit operator Guid(TaskItemId taskItemId) => taskItemId.Value;
    public static implicit operator TaskItemId(Guid taskItemId) => new(taskItemId);

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}