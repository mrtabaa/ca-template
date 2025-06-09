using System.Security.Claims;
using Ca.Domain.Modules.Auth.Enums;
using Ca.Domain.Modules.Seed;
using Ca.Infrastructure.Modules.Auth.Mongo.Models;
using Microsoft.AspNetCore.Identity;

namespace Ca.Infrastructure.Modules.Seed.Mongo;

public class RoleSeederRepositoryMongo(RoleManager<AppRoleMongo> roleManager) : IRoleSeederRepository
{
    public async Task<bool> SeedRolesAndPermissionsAsync()
    {
        bool isSucceeded = await EnsureRoleWithPermissions(
            Role.Admin, [
                Permission.BanUser,
                Permission.DeleteUser,
                Permission.AccessAdminPanel
            ]
        );
        if (!isSucceeded) return false;

        isSucceeded = await EnsureRoleWithPermissions(
            Role.Moderator, [
                Permission.BanUser,
                Permission.ApprovePhotos
            ]
        );
        if (!isSucceeded) return false;

        isSucceeded = await EnsureRoleWithPermissions(
            Role.Parking, [
                Permission.AccessParkingPanel,
                Permission.SendMessages,
                Permission.EditOwnProfile
            ]
        );
        if (!isSucceeded) return false;

        isSucceeded = await EnsureRoleWithPermissions(
            Role.Client, [
                Permission.SendMessages,
                Permission.EditOwnProfile
            ]
        );
        return isSucceeded;
    }

    private async Task<bool> EnsureRoleWithPermissions(Role roleEnum, Permission[] permissions)
    {
        var roleName = roleEnum.ToString();
        AppRoleMongo role = await roleManager.FindByNameAsync(roleName);
        if (role is null)
        {
            role = new AppRoleMongo { Name = roleName };
            IdentityResult? result = await roleManager.CreateAsync(role);

            if (!result.Succeeded) return false;
        }

        IList<Claim>? existingClaims = await roleManager.GetClaimsAsync(role);

        foreach (Permission permission in permissions)
            if (!existingClaims.Any(claim => claim.Type == nameof(permission) && claim.Value == permission.ToString()
                ))
            {
                IdentityResult? result = await roleManager.AddClaimAsync(
                    role,
                    new Claim(permission.ToString(), permission.ToString())
                );

                if (!result.Succeeded) return false;
            }

        return true;
    }
}