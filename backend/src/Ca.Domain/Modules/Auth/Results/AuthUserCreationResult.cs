using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Domain.Modules.Auth.Enums;

namespace Ca.Domain.Modules.Auth.Results;

public record AuthUserCreationResult(
    bool Succeeded,
    AppUser? AppUser,
    AuthUserCreationErrorType ErrorType,
    string? ErrorMessage
);