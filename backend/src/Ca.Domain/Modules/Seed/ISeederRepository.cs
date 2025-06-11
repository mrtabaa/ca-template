using Ca.Domain.Modules.Auth.Aggregates;

namespace Ca.Domain.Modules.Seed;

public interface ISeederRepository
{
    public Task SeedSuperAdminAsync(AppUser appUser);

    public Task SeedRolesAndPermissionsAsync();
}