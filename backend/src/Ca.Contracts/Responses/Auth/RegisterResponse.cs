namespace Ca.Contracts.Responses.Auth;

public record RegisterResponse
(
    string Name,
    string Email,
    bool IsAlive
);