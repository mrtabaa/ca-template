using Ca.Application.Modules.Auth.Commands;
using Ca.Contracts.Responses.Auth;
using Ca.Domain.Modules.AccessControl.Enums;
using Ca.Domain.Modules.Auth;
using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Domain.Modules.Auth.Results;
using Ca.Shared.Results;

namespace Ca.Application.Modules.Auth;

public class AuthService(IAuthRepository authRepository) : IAuthService
{
    public async Task<OperationResult<RegisterResponse>> CreateAsync(RegisterCommand command)
    {
        var appUser = AppUser.Create( // Aggregate 
            command.FirstName, command.LastName, command.Email, command.UserName, command.Password
        );

        AuthUserCreationResult result = await authRepository.CreateAppUserAsync(appUser, AccessRoleType.Client);

        return AuthMapper.MapAppUserToRegisterResult(result);
    }

    public async Task<OperationResult<RegisterResponse>> SeedSuperAdminAppUserAsync(RegisterSuperAdminCommand command)
    {
        var appUser = AppUser.CreateSuperAdmin( // It guards from creating extra super admins
            command.FirstName, command.LastName, command.Email, command.UserName, command.RoleNameRaw, command.Password
        );

        AuthUserCreationResult result = await authRepository.SeedSuperAdminAppUserAsync(
            appUser, AccessRoleType.SuperAdmin
        );

        return AuthMapper.MapAppUserToRegisterResult(result);
    }
}