namespace Ca.Domain.Modules.Auth.Entities;

public record AppUser
(
    string Name,
    string Email,
    string Password,
    bool IsAlive
);