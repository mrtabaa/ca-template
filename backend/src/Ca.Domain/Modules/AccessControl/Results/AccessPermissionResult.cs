namespace Ca.Domain.Modules.AccessControl.Results;

public record AccessPermissionResult(
    bool Succeeded,
    string? ErrorMessage
);