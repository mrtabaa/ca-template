namespace Ca.Domain.Modules.Seeder;

public interface IRoleSeederRepository
{
    public Task<bool> SeedRolesAndPermissionsAsync();
}