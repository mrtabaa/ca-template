using Ca.Domain.Modules.Auth;
using Ca.Domain.Modules.Auth.Entities;
using Ca.Domain.Modules.Auth.Results;

namespace Ca.Infrastructure.Modules.Payment.PostgreSql;

public class PostgresPaymentRepository : IAuthRepository
{
    public async Task<AuthCreationResponse> CreateAsync(AppUser appUser, CancellationToken cancellationToken) => 
        throw new NotImplementedException();
}