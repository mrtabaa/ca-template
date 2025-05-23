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
        var appUser = AppUser.Create(command.Name, command.Email, command.Password);

        AuthCreationResponse response = await authRepository.CreateAsync(appUser, ct);

        return AuthMapper.MapAppUserToRegisterResult(response);
    }
}