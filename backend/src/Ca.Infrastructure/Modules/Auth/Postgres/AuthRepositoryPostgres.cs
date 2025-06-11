using Ca.Domain.Modules.AccessControl.Enums;
using Ca.Domain.Modules.Auth;
using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Domain.Modules.Auth.Results;

namespace Ca.Infrastructure.Modules.Auth.Postgres;

public class AuthRepositoryPostgres : IAuthRepository
{
    public Task<AuthUserCreationResult> CreateAppUserAsync(AppUser appUser, AccessRoleType roleType) =>
        throw new NotImplementedException();

    public Task<AuthUserCreationResult> SeedSuperAdminAppUserAsync(AppUser appUser, AccessRoleType roleType) =>
        throw new NotImplementedException();
}