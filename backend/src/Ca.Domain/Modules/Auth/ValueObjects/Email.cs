using Ca.Domain.Modules.Common.Base;
using Ca.Domain.Modules.Common.Exceptions;
using Ca.Domain.Modules.Common.Validations;
using Ca.Domain.Shared;

namespace Ca.Domain.Modules.Auth.ValueObjects;

public sealed class Email : ValueObject
{
    private Email(string value) => Value = value;
    
    public string Value { get; }

    public static Email Create(string? emailRaw)
    {
        string? validationError = emailRaw.ValidateValue(
            nameof(emailRaw), minLength: -1, SharedLengths.EmailMax,
            @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$"
        );

        if (validationError is not null)
            throw new DomainException(validationError);

        return new Email(emailRaw!.Trim().ToLowerInvariant());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}