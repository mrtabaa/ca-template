using Ca.Domain.Modules.Common.Base;
using Ca.Domain.Modules.Common.Exceptions;
using Ca.Domain.Modules.Common.Validations;
using Ca.Domain.Shared;

namespace Ca.Domain.Modules.Auth.ValueObjects;

public sealed class FirstName : ValueObject
{
    private FirstName(string value) => Value = value;

    public string Value { get; }

    public static FirstName Create(string? firstNameRaw)
    {
        string? validationError = firstNameRaw.ValidateValue(
            nameof(firstNameRaw), CustomLengths.NameMin, CustomLengths.NameMax
        );

        if (validationError is not null)
            throw new DomainException(validationError);

        return new FirstName(firstNameRaw!.Trim());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}