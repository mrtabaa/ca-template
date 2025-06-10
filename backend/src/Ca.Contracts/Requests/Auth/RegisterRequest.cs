namespace Ca.Contracts.Requests.Auth;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    string ConfirmPassword
);