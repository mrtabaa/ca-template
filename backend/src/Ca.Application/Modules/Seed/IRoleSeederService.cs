using Ca.Domain.Modules.Seed.Results;

namespace Ca.Application.Modules.Seed;

public interface IRoleSeederService
{
    public Task<SeedRolesResponse> SeedRolesAndPermissionsAsync();
}