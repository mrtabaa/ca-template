namespace Ca.Domain.Modules.Seed;

public interface IRoleSeederRepository
{
    public Task<bool> SeedRolesAndPermissionsAsync();
}