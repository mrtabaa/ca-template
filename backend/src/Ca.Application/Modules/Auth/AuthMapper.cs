using Ca.Contracts.Responses.Auth;
using Ca.Domain.Modules.Auth.Enums;
using Ca.Domain.Modules.Auth.Results;
using Ca.Shared.Results;

namespace Ca.Application.Modules.Auth;

public static class AuthMapper
{
    public static OperationResult<RegisterResponse> MapAppUserToRegisterResult(AuthCreationResponse authResponse) =>
        authResponse.AppUser is not null
            ? new OperationResult<RegisterResponse>(
                true,
                new RegisterResponse(
                    authResponse.AppUser.Name,
                    authResponse.AppUser.Email.Value,
                    authResponse.AppUser.IsAlive
                ),
                null
            )
            : authResponse.AuthCreationResult switch
            {
                AuthCreationResult.EmailAlreadyExists => new OperationResult<RegisterResponse>(
                    false,
                    Error: new CustomError(ResultErrorCode.IsEmailTaken, "User already exists.")
                ),
                _ => new OperationResult<RegisterResponse>(
                    false,
                    Error: new CustomError(ResultErrorCode.NetIdentityFailed, "Creation failed.")
                )
            };
}