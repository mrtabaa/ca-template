using Ca.Domain.Modules.Auth;
using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Domain.Modules.Auth.Results;

namespace Ca.Infrastructure.Modules.Payment.Postgres;

public class PaymentRepositoryPostgres : IAuthRepository
{
    public async Task<AuthCreationResponse> CreateAsync(AppUser appUser, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}