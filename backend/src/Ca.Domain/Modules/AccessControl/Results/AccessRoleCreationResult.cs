namespace Ca.Domain.Modules.AccessControl.Results;

public record AccessRoleCreationResult(
    bool Succeeded,
    string? ErrorMessage
);