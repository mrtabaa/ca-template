using Ca.Domain.Modules.Common.Base;
using Ca.Domain.Modules.Common.Exceptions;
using Ca.Domain.Modules.Common.Validations;
using Ca.Domain.Shared;

namespace Ca.Domain.Modules.Auth.ValueObjects;

public sealed class LastName : ValueObject
{
    private LastName(string value) => Value = value;

    public string Value { get; }

    public static LastName Create(string? lastNameRaw)
    {
        string? validationError = lastNameRaw.ValidateValue(
            nameof(lastNameRaw), CustomLengths.NameMin, CustomLengths.NameMax
        );

        if (validationError is not null)
            throw new DomainException(validationError);

        return new LastName(lastNameRaw!.Trim());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}