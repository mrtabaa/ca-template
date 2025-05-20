using Ca.Application.Modules.Auth.Commands;
using Ca.Contracts.Requests.Auth;

namespace Ca.WebApi.Modules.Auth;

public static class AuthRequestMapper
{
    internal static RegisterCommand MapRegisterRequestToRegisterCommand(RegisterRequest request) =>
        new RegisterCommand(
            Name: request.Name,
            Email: request.Email,
            Password: request.Password,
            IsAlive: request.IsAlive
        );
}