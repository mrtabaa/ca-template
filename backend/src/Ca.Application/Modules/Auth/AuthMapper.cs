using Ca.Contracts.Responses.Auth;
using Ca.Domain.Modules.Auth.Enums;
using Ca.Domain.Modules.Auth.Results;
using Ca.Shared.Results;

namespace Ca.Application.Modules.Auth;

public static class AuthMapper
{
    public static OperationResult<RegisterResponse> MapAppUserToRegisterResult(AuthUserCreationResult authUserResult) =>
        authUserResult.AppUser is not null
            ? new OperationResult<RegisterResponse>(
                IsSuccess: true,
                new RegisterResponse(
                    authUserResult.AppUser.Name.Value,
                    authUserResult.AppUser.LastName.Value,
                    authUserResult.AppUser.Email.Value,
                    authUserResult.AppUser.UserName.Value
                ),
                Error: null
            )
            : authUserResult.AuthUserCreationStatus switch
            {
                AuthUserCreationStatus.EmailAlreadyExists => new OperationResult<RegisterResponse>(
                    IsSuccess: false,
                    Error: new CustomError(ResultErrorCode.IsEmailAlreadyConfirmed, "User already exists.")
                ),
                _ => new OperationResult<RegisterResponse>(
                    IsSuccess: false,
                    Error: new CustomError(ResultErrorCode.IsEmailAlreadyConfirmed, "Creation failed.")
                )
            };
}