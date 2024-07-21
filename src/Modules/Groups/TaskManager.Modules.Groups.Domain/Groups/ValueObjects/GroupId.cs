using TaskManager.Abstractions.Kernel.Primitives;

namespace TaskManager.Modules.Groups.Domain.Groups.ValueObjects;

public record GroupId : EntityId
{
    public GroupId(Guid value) : base(value)
    {
    }

    public static GroupId New() => new(Guid.NewGuid());
    public static GroupId From(Guid value) => new(value);
    public static GroupId From(string value) => new(Guid.Parse(value));

    public static implicit operator Guid(GroupId groupId) => groupId.Value;
    public static implicit operator GroupId(Guid userId) => new(userId);

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}