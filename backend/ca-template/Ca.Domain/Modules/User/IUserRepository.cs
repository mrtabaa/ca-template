using Ca.Domain.Modules.Auth.Aggregates;

namespace Ca.Domain.Modules.User;

public interface IUserRepository
{
    public Task<AppUser?> GetUserByIdAsync(string idStr, CancellationToken ct);
    public Task<bool> ChangeUserNameAsync(string idStr, string newName, CancellationToken ct);
}