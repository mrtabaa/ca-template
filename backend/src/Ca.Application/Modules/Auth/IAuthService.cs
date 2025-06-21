using Ca.Application.Modules.Auth.Commands;
using Ca.Contracts.Responses.Auth;
using Ca.Shared.Results;

namespace Ca.Application.Modules.Auth;

public interface IAuthService
{
    public Task<OperationResult<RegisterResponse>> CreateAsync(RegisterCommand command);
    public Task<OperationResult<RegisterResponse>> SeedSuperAdminAppUserAsync(RegisterSuperAdminCommand command);
}