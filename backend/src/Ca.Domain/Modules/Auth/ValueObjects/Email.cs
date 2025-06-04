using System.Text.RegularExpressions;
using Ca.Domain.Modules.Common.Base;

namespace Ca.Domain.Modules.Auth.ValueObjects;

public class Email : ValueObject
{
    private Email(string value) => Value = value;

    public string Value { get; }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$"))
            throw new Exception("Invalid email.");

        return new Email(value.Trim().ToLowerInvariant());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}