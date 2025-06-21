using Ca.Contracts.Responses.Auth;
using Ca.Domain.Modules.Auth.Enums;
using Ca.Domain.Modules.Auth.Results;
using Ca.Shared.Results;

namespace Ca.Application.Modules.Auth;

internal static class AuthMapper
{
    internal static OperationResult<RegisterResponse> MapAppUserToRegisterResult(AuthUserCreationResult result) =>
        result.Succeeded && result.AppUser is not null
            ? new OperationResult<RegisterResponse>(
                result.Succeeded,
                new RegisterResponse(
                    result.AppUser.Name.Value,
                    result.AppUser.LastName.Value,
                    result.AppUser.Email.Value,
                    result.AppUser.UserName.Value
                ),
                Error: null
            )
            : result.ErrorType switch
            {
                AuthUserCreationErrorType.EmailAlreadyExists => new OperationResult<RegisterResponse>(
                    IsSuccess: false,
                    Error: new CustomError(AuthUserCreationErrorType.EmailAlreadyExists, result.ErrorMessage)
                ),
                AuthUserCreationErrorType.UsernameAlreadyExists => new OperationResult<RegisterResponse>(
                    IsSuccess: false,
                    Error: new CustomError(AuthUserCreationErrorType.UsernameAlreadyExists, result.ErrorMessage)
                ),
                AuthUserCreationErrorType.AddRoleFailed => new OperationResult<RegisterResponse>(
                    IsSuccess: false,
                    Error: new CustomError(AuthUserCreationErrorType.AddRoleFailed, result.ErrorMessage)
                ),
                _ => new OperationResult<RegisterResponse>(
                    IsSuccess: false,
                    Error: new CustomError(AuthUserCreationErrorType.Unknown, result.ErrorMessage)
                )
            };
}