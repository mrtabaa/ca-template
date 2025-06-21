using Ca.Application.Modules.Auth.Commands;
using Ca.Contracts.Requests.Auth;
using Ca.Shared.Configurations.Common.SeedSettings;

namespace Ca.WebApi.Modules.Auth;

internal static class AuthRequestMapper
{
    internal static RegisterCommand MapRegisterRequestToRegisterCommand(RegisterRequest request) =>
        new(
            request.FirstName,
            request.LastName,
            request.Email,
            request.UserName,
            request.Password
        );

    internal static RegisterSuperAdminCommand
        MapRegisterSuperAdminRequestToRegisterCommand(SuperAdminSeedInfo request) =>
        new(
            request.FirstName,
            request.LastName,
            request.Email,
            request.UserName,
            request.RoleName,
            request.Password
        );
}