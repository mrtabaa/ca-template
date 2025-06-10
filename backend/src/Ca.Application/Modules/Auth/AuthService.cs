using Ca.Application.Modules.Auth.Commands;
using Ca.Contracts.Responses.Auth;
using Ca.Domain.Modules.Auth;
using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Domain.Modules.Auth.Results;
using Ca.Shared.Results;

namespace Ca.Application.Modules.Auth;

public class AuthService(IAuthRepository authRepository) : IAuthService
{
    public async Task<OperationResult<RegisterResponse>> CreateAsync(
        RegisterCommand command, CancellationToken ct
    )
    {
        var appUser = AppUser.Create(
            command.FirstName, command.LastName, command.Email, command.UserName, command.Password
        );

        AuthCreationResult result = await authRepository.CreateAsync(appUser, ct);

        return AuthMapper.MapAppUserToRegisterResult(result);
    }
}