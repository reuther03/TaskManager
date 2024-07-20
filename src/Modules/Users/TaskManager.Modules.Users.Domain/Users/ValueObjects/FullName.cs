using TaskManager.Abstractions.Kernel.Primitives;

namespace TaskManager.Modules.Users.Domain.Users.ValueObjects;

public sealed record FullName : ValueObject
{
    public string Value { get; set; }

    public FullName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 100 or < 3)
        {
            throw new ArgumentException("FullName cannot be empty or ", nameof(value));
        }

        Value = value;
    }

    public static implicit operator FullName(string value) => new(value);
    public static implicit operator string(FullName fullname) => fullname.Value;

    public override string ToString() => Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}