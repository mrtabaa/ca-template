using Ca.Domain.Modules.Auth;
using Ca.Domain.Modules.Auth.Entities;
using Ca.Domain.Modules.Auth.Results;

namespace Ca.Infrastructure.Modules.Message.Postgres;

public class PostgresMessageRepository : IAuthRepository
{
    public async Task<AuthCreationResponse> CreateAsync(AppUser appUser, CancellationToken cancellationToken) => 
        throw new NotImplementedException();
}