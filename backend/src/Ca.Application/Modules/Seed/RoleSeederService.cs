using Ca.Domain.Modules.Seed;
using Ca.Domain.Modules.Seed.Results;

namespace Ca.Application.Modules.Seed;

public class RoleSeederService(IRoleSeederRepository roleSeederRepository) : IRoleSeederService
{
    public async Task<SeedRolesResponse> SeedRolesAndPermissionsAsync() =>
        new(await roleSeederRepository.SeedRolesAndPermissionsAsync());
}