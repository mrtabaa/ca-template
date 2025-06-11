using Ca.Application.Modules.Auth.Commands;
using Ca.Contracts.Responses.Auth;
using Ca.Domain.Modules.AccessControl.Enums;
using Ca.Domain.Modules.Auth;
using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Domain.Modules.Auth.Results;
using Ca.Shared.Configurations.Common.SeedSettings;
using Ca.Shared.Results;
using Microsoft.Extensions.Options;

namespace Ca.Application.Modules.Auth;

public class AuthService(IAuthRepository authRepository, IOptions<SuperAdminSeedInfo> superAdminSeedInfo) : IAuthService
{
    public async Task<OperationResult<RegisterResponse>> CreateAsync(RegisterCommand command)
    {
        var appUser = AppUser.Create(
            command.FirstName, command.LastName, command.Email, command.UserName, command.Password
        );

        AuthUserCreationResult result = await authRepository.CreateAppUserAsync(appUser, AccessRoleType.Client);

        return AuthMapper.MapAppUserToRegisterResult(result);
    }

    public async Task<OperationResult<RegisterResponse>> SeedSuperAdminAppUserAsync()
    {
        SuperAdminSeedInfo seedInfo = superAdminSeedInfo.Value;

        var appUser = AppUser.CreateSuperAdmin( // It guards from creating extra super admins
            seedInfo.FirstName, seedInfo.LastName, seedInfo.Email, seedInfo.UserName, seedInfo.RoleName,
            seedInfo.Password
        );

        AuthUserCreationResult result = await authRepository.SeedSuperAdminAppUserAsync(
            appUser, AccessRoleType.SuperAdmin
        );

        return AuthMapper.MapAppUserToRegisterResult(result);
    }
}