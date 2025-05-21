using Ca.Domain.Modules.Auth;
using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Domain.Modules.Auth.Results;

namespace Ca.Infrastructure.Modules.Auth.Postgres;

public class AuthRepositoryPostgres : IAuthRepository
{
    public async Task<AuthCreationResponse> CreateAsync(AppUser appUser, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}