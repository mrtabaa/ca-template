using System.Security.Claims;
using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Domain.Modules.Auth.Enums;
using Ca.Domain.Modules.Seed;
using Ca.Domain.Shared;
using Ca.Infrastructure.Modules.Auth.Mongo.Models;
using Ca.Infrastructure.Modules.Common.Mongo;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Ca.Infrastructure.Modules.Seed.Mongo;

public class SeederRepositoryMongo(
    UserManager<AppUserMongo> userManager,
    RoleManager<AppRoleMongo> roleManager,
    ILogger<SeederRepositoryMongo> logger
)
    : ISeederRepository
{
    public async Task SeedSuperAdminAsync(AppUser appUser)
    {
        AppUserMongo? existingSuperAdmin = await userManager.FindByEmailAsync(appUser.Email?.Value);
        if (existingSuperAdmin != null)
        {
            logger.LogWarning("SuperAdmin exists already.");
            return;
        }

        AppUserMongo appUserMongo = CommonMapperMongo.MapAppUserToAppUserMongo(appUser);

        IdentityResult result = await userManager.CreateAsync(appUserMongo, appUser.Password?.Value);

        if (!result.Succeeded)
            logger.LogError(result.Errors.FirstOrDefault()?.Description);
    }

    public async Task SeedRolesAndPermissionsAsync()
    {
        await EnsureRoleWithPermissions(
            Role.SuperAdmin, [
                Permission.AccessSuperAdminPanel
            ]
        );
        
        await EnsureRoleWithPermissions(
            Role.Admin, [
                Permission.BanUser,
                Permission.DeleteUser,
                Permission.AccessAdminPanel
            ]
        );

        await EnsureRoleWithPermissions(
            Role.Moderator, [
                Permission.BanUser,
                Permission.ApprovePhotos
            ]
        );

        await EnsureRoleWithPermissions(
            Role.Parking, [
                Permission.AccessParkingPanel,
                Permission.SendMessages,
                Permission.EditOwnProfile
            ]
        );

        await EnsureRoleWithPermissions(
            Role.Client, [
                Permission.SendMessages,
                Permission.EditOwnProfile
            ]
        );
    }

    private async Task EnsureRoleWithPermissions(Role roleEnum, Permission[] permissions)
    {
        IdentityResult? result = null;

        var roleName = roleEnum.ToString();
        AppRoleMongo role = await roleManager.FindByNameAsync(roleName);
        if (role is null)
        {
            role = new AppRoleMongo { Name = roleName };
            result = await roleManager.CreateAsync(role);

            if (!result.Succeeded)
                logger.LogError(result.Errors.FirstOrDefault()?.Description);
        }

        IList<Claim>? existingClaims = await roleManager.GetClaimsAsync(role);

        // CustomClaimTypes.Permission == "permission" created for consistency
        foreach (Permission permission in permissions)
            if (!existingClaims.Any(claim => claim.Type == CustomClaimTypes.Permission &&
                                             claim.Value == permission.ToString()
                ))
            {
                result = await roleManager.AddClaimAsync(
                    role,
                    new Claim(CustomClaimTypes.Permission, permission.ToString())
                );

                if (!result.Succeeded)
                    logger.LogError(result.Errors.FirstOrDefault()?.Description);
            }
    }
}