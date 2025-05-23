namespace Ca.Contracts.Requests.Auth;

public record RegisterRequest
(
    string Name,
    string Email,
    string Password,
    string ConfirmPassword,
    bool IsAlive
);