using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Domain.Modules.Seed;
using Ca.Shared.Configurations.Common.SeedSettings;
using Microsoft.Extensions.Options;

namespace Ca.Application.Modules.Seed;

public class SeederService(ISeederRepository seederRepository, IOptions<AppAdminSeedInfo> appAdminSeedInfo)
    : ISeederService
{
    public async Task SeedAppAdminAsync()
    {
        AppAdminSeedInfo seedInfo = appAdminSeedInfo.Value;

        var appUser = AppUser.Create(
            seedInfo.FirstName, seedInfo.LastName, seedInfo.Email, seedInfo.UserName, seedInfo.Password
        );

        await seederRepository.SeedSuperAdminAsync(appUser);
    }

    public async Task SeedRolesAndPermissionsAsync()
    {
        await seederRepository.SeedRolesAndPermissionsAsync();
    }
}