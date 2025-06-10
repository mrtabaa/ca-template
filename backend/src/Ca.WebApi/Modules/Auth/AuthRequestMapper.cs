using Ca.Application.Modules.Auth.Commands;
using Ca.Contracts.Requests.Auth;

namespace Ca.WebApi.Modules.Auth;

public static class AuthRequestMapper
{
    internal static RegisterCommand MapRegisterRequestToRegisterCommand(RegisterRequest request) =>
        new(
            request.FirstName,
            request.LastName,
            request.Email,
            request.UserName,
            request.Password
        );
}