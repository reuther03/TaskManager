using TaskManager.Abstractions.Kernel.Primitives;

namespace TaskManager.Modules.Management.Domain.Teams;

public record TeamId : EntityId
{
    public TeamId(Guid value) : base(value)
    {
    }

    public static TeamId New() => new(Guid.NewGuid());
    public static TeamId From(Guid value) => new(value);
    public static TeamId From(string value) => new(Guid.Parse(value));

    public static implicit operator Guid(TeamId teamId) => teamId.Value;
    public static implicit operator TeamId(Guid teamId) => new(teamId);
    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}