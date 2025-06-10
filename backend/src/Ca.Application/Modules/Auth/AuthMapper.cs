using Ca.Contracts.Responses.Auth;
using Ca.Domain.Modules.Auth.Enums;
using Ca.Domain.Modules.Auth.Results;
using Ca.Shared.Results;

namespace Ca.Application.Modules.Auth;

public static class AuthMapper
{
    public static OperationResult<RegisterResponse> MapAppUserToRegisterResult(AuthCreationResult authResult) =>
        authResult.AppUser is not null
            ? new OperationResult<RegisterResponse>(
                IsSuccess: true,
                new RegisterResponse(
                    authResult.AppUser.FirstName.Value,
                    authResult.AppUser.LastName.Value,
                    authResult.AppUser.Email.Value,
                    authResult.AppUser.UserName.Value
                ),
                Error: null
            )
            : authResult.AuthCreationStatus switch
            {
                AuthCreationStatus.EmailAlreadyExists => new OperationResult<RegisterResponse>(
                    IsSuccess: false,
                    Error: new CustomError(ResultErrorCode.IsEmailAlreadyConfirmed, "User already exists.")
                ),
                _ => new OperationResult<RegisterResponse>(
                    IsSuccess: false,
                    Error: new CustomError(ResultErrorCode.IsEmailAlreadyConfirmed, "Creation failed.")
                )
            };
}