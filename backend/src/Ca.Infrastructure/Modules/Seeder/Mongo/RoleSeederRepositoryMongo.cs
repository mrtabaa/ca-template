using System.Security.Claims;
using Ca.Domain.Modules.Auth.Enums;
using Ca.Domain.Modules.Seeder;
using Microsoft.AspNetCore.Identity;

namespace Ca.Infrastructure.Modules.Seeder.Mongo;

public class RoleSeederRepositoryMongo(RoleManager<IdentityRole> roleManager) : IRoleSeederRepository
{
    public async Task<bool> SeedRolesAndPermissionsAsync() => true;

    private async Task EnsureRoleWithPermissions(Role roleEnum, Permission[] permissions)
    {
        var roleName = roleEnum.ToString();
        IdentityRole role = await roleManager.FindByNameAsync(roleName);
        if (role is null)
        {
            role = new IdentityRole(roleName);
            await roleManager.CreateAsync(role);
        }

        IList<Claim>? existingClaims = await roleManager.GetClaimsAsync(role);

        foreach (Permission permission in permissions)
            if (!existingClaims.Any(claim => claim.Type == nameof(permission) && claim.Value == permission.ToString()
                ))
            {
                await roleManager.AddClaimAsync(
                    role,
                    new Claim(permission.ToString(), permission.ToString())
                );
            }
    }
}