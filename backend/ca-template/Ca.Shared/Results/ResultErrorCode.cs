namespace Ca.Shared.Results;

public enum ResultErrorCode
{
    IsRecaptchaTokenInvalid,
    IsEmailAlreadyConfirmed,
    IsEmailTaken,
    IsUsernameTaken,
    IsWrongCreds,
    IsSessionExpired,
    NetIdentityFailed,
    IsEmailNotConfirmed
}