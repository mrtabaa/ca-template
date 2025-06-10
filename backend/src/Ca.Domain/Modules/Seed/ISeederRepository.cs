using Ca.Domain.Modules.Auth.Aggregates;

namespace Ca.Domain.Modules.Seed;

public interface ISeederRepository
{
    public Task SeedAppAdminAsync(AppUser appUser);

    public Task SeedRolesAndPermissionsAsync();
}