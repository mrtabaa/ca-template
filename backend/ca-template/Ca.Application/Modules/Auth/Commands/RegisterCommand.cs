namespace Ca.Application.Modules.Auth.Commands;

public record RegisterCommand
(
    string Name,
    string Email,
    string Password,
    bool IsAlive
);