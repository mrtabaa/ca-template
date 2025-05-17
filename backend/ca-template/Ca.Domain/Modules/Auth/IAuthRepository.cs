using Ca.Domain.Modules.Auth.Entities;
using Ca.Domain.Modules.Auth.Results;

namespace Ca.Domain.Modules.Auth;

public interface IAuthRepository
{
    public Task<AuthCreationResponse> CreateAsync(AppUser appUser, CancellationToken cancellationToken);

    // public Task<OperationResult<LoginResult>> VerifyAsync(
    //     VerifyDto verifyDto, SessionMetadata sessionMetadata, CancellationToken cancellationToken
    // );
    //
    // public Task<OperationResult> ResendVerifyCodeAsync(
    //     ResendCodeRequest resendCodeRequest, CancellationToken cancellationToken
    // );
    //
    // public Task<OperationResult<LoginResult>> LoginAsync(
    //     LoginDto loginDto, SessionMetadata sessionMetadata, CancellationToken cancellationToken
    // );
    //
    // public Task<OperationResult<TokenDto>> RefreshTokensAsync(
    //     RefreshTokenRequest refreshTokenRequest, CancellationToken cancellationToken
    // );
    //
    // public Task<OperationResult<LoggedInDto>> ReloadLoggedInUserAsync(
    //     string userIdHashed, CancellationToken cancellationToken
    // );
    //
    // public Task<OperationResult> RequestResetPasswordAsync(
    //     ResetPasswordRequest request, CancellationToken cancellationToken
    // );
    //
    // public Task<OperationResult> ResetPasswordAsync(ResetPassword resetPassword, CancellationToken cancellationToken);
    // public Task<OperationResult<DeleteResult>> DeleteUserAsync(string? userEmail, CancellationToken cancellationToken);
    //
    // public Task<OperationResult<bool>> UpdateLastActive(
    //     string loggedInUserIdHashed, CancellationToken cancellationToken
    // );
}