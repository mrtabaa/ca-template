using Ca.Domain.Modules.Shared.Base;

namespace Ca.Domain.Modules.Auth.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
            throw new Exception("Invalid email");

        return new Email(value.Trim().ToLowerInvariant());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}