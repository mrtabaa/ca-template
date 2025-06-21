using Ca.Application.Modules.Auth;
using Ca.Application.Modules.Auth.Commands;
using Ca.Contracts.Requests.Auth;
using Ca.Contracts.Responses.Auth;
using Ca.Domain.Modules.Auth.Enums;
using Ca.Shared.Results;
using Ca.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Ca.WebApi.Modules.Auth;

public class AuthController(IAuthService authService) : BaseApiController
{
    public async Task<ActionResult<RegisterResponse>> Create(RegisterRequest request, CancellationToken ct)
    {
        if (request.Password != request.ConfirmPassword)
            return BadRequest("Passwords do not match!");

        RegisterCommand command = AuthRequestMapper.MapRegisterRequestToRegisterCommand(request);

        OperationResult<RegisterResponse> result = await authService.CreateAsync(command);

        return result.IsSuccess
            ? result.Result
            : result.Error?.Code switch
            {
                AuthUserCreationErrorType.EmailAlreadyExists => BadRequest(result.Error.Message),
                AuthUserCreationErrorType.UsernameAlreadyExists => BadRequest(result.Error.Message),
                AuthUserCreationErrorType.AddRoleFailed => BadRequest(result.Error.Message),
                _ => BadRequest("Account creation failed.")
            };
    }
}