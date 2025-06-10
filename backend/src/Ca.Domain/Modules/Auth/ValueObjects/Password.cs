using Ca.Domain.Shared;

namespace Ca.Domain.Modules.Auth.ValueObjects;

public sealed class Password
{
    private Password(string value) => Value = value;

    public string Value { get; }

    public static Password Create(string value)
    {
        if (
            string.IsNullOrEmpty(value)
            || value.Length < CustomLengths.PasswordMin
            || value.Length > CustomLengths.PasswordMax
        )
            throw new Exception("Password value must be between 8 and 30 characters");

        return new Password(value);
    }

    //overrides the default ToString() behavior to hide the actual password value when the object is printed or logged.
    public override string ToString() => "[PROTECTED]";
}