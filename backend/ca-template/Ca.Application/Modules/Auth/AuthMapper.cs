using Ca.Application.Modules.Auth.Commands;
using Ca.Contracts.Responses.Auth;
using Ca.Domain.Modules.Auth.Entities;
using Ca.Domain.Modules.Auth.Enums;
using Ca.Domain.Modules.Auth.Results;
using Ca.Shared.Results;

namespace Ca.Application.Modules.Auth;

public static class AuthMapper
{
    public static AppUser MapRegisterCommandToAppUser(RegisterCommand command) =>
        new AppUser(
            Name: command.Name,
            Email: command.Email,
            Password: command.Password,
            IsAlive: command.IsAlive
        );

    public static OperationResult<RegisterResponse> MapAppUserToRegisterResult(AuthCreationResponse authResponse) =>
        authResponse.AppUser is not null
            ? new OperationResult<RegisterResponse>(
                IsSuccess: true,
                Result:  new RegisterResponse(
                    Name: authResponse.AppUser.Name,
                    Email: authResponse.AppUser.Email,
                    IsAlive: authResponse.AppUser.IsAlive
                ),
                Error: null
            )
            : authResponse.AuthCreationResult switch
            {
                AuthCreationResult.EmailAlreadyExists => new OperationResult<RegisterResponse>(
                    IsSuccess: false,
                    Error: new CustomError(ResultErrorCode.IsEmailAlreadyConfirmed, "User already exists.")
                ),
                _ => new OperationResult<RegisterResponse>(
                    IsSuccess: false,
                    Error: new CustomError(ResultErrorCode.IsEmailAlreadyConfirmed, "Creation failed.")
                ),
            };
}