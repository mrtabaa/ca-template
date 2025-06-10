using Ca.Domain.Modules.Common.Base;
using Ca.Domain.Modules.Common.Exceptions;
using Ca.Domain.Modules.Common.Validations;
using Ca.Domain.Shared;

namespace Ca.Domain.Modules.Auth.ValueObjects;

public sealed class UserName : ValueObject
{
    private UserName(string value) => Value = value;

    public string Value { get; }

    public static UserName Create(string? userNameRaw)
    {
        string? validationError = userNameRaw.ValidateValue(
            nameof(userNameRaw), CustomLengths.NameMin, CustomLengths.NameMax
        );

        if (validationError is not null)
            throw new DomainException(validationError);

        return new UserName(userNameRaw!.Trim());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}