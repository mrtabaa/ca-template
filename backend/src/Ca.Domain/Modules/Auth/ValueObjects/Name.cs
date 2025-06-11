using Ca.Domain.Modules.Common.Base;
using Ca.Domain.Modules.Common.Exceptions;
using Ca.Domain.Modules.Common.Validations;
using Ca.Domain.Shared;

namespace Ca.Domain.Modules.Auth.ValueObjects;

public sealed class Name : ValueObject
{
    private Name(string value) => Value = value;

    public string Value { get; }

    public static Name Create(string? nameRaw)
    {
        string? validationError = nameRaw.ValidateValue(
            nameof(nameRaw), CustomLengths.NameMin, CustomLengths.NameMax
        );

        if (validationError is not null)
            throw new DomainException(validationError);

        return new Name(nameRaw!.Trim());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}