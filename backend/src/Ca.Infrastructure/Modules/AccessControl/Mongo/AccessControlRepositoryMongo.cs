using System.Security.Claims;
using Ca.Domain.Modules.AccessControl;
using Ca.Domain.Modules.AccessControl.Aggregates;
using Ca.Domain.Modules.AccessControl.Results;
using Ca.Domain.Modules.AccessControl.ValueObjects;
using Ca.Domain.Shared;
using Ca.Infrastructure.Modules.AccessControl.Mongo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Ca.Infrastructure.Modules.AccessControl.Mongo;

public class AccessControlRepositoryMongo(
    RoleManager<AppRoleMongo> roleManager,
    ILogger<AccessControlRepositoryMongo> logger
) : IAccessControlRepository
{
    /// <summary>
    ///     Create a new role and assign permissions to it.
    /// </summary>
    /// <param name="appRole"></param>
    /// <returns>AccessRoleCreationResult</returns>
    /// <exception cref="ApplicationException"></exception>
    public async Task<AccessRoleCreationResult> CreateAppRoleAsync(AppRole appRole)
    {
        AppRoleMongo appRoleMongo = await roleManager.FindByNameAsync(appRole.Name.Value);
        if (appRoleMongo is null)
        {
            appRoleMongo = new AppRoleMongo { Name = appRole.Name.Value };

            IdentityResult result = await roleManager.CreateAsync(appRoleMongo);
            if (!result.Succeeded)
            {
                string? roleError = result.Errors.FirstOrDefault()?.Description;
                logger.LogError(roleError);
                return new AccessRoleCreationResult(Succeeded: false, roleError);
            }
        }
        
        // Manage Permissions
        string? permissionError = await ManagePermissionsAsync(appRoleMongo);
        if (permissionError is not null)
        {
            IdentityResult deleteResult = await roleManager.DeleteAsync(appRoleMongo);
            if (!deleteResult.Succeeded)
            {
                string? deleteError = deleteResult.Errors.FirstOrDefault()?.Description;
                logger.LogError(deleteError);
                return new AccessRoleCreationResult(Succeeded: false, deleteError);
            }

            logger.LogError(permissionError);
            return new AccessRoleCreationResult(Succeeded: false, permissionError);
        }

        return new AccessRoleCreationResult(Succeeded: true, ErrorMessage: null);
    }

    /// <summary>
    ///     Assigns permissions to a role.
    /// </summary>
    /// <param name="appRoleMongo"></param>
    /// <returns>string: Error message returned while adding the claim, if any.</returns>
    private async Task<string?> ManagePermissionsAsync(AppRoleMongo appRoleMongo)
    {
        IList<Claim>? existingClaims = await roleManager.GetClaimsAsync(appRoleMongo);

        // CustomVariableNames.Permission == "accessPermissionType" created for consistency
        foreach (Permission permission in appRoleMongo.Permissions)
            if (!existingClaims.Any(claim => claim.Type == CustomVariableNames.Permission &&
                                             claim.Value == permission.ToString()
                ))
            {
                IdentityResult result = await roleManager.AddClaimAsync(
                    appRoleMongo,
                    new Claim(CustomVariableNames.Permission, permission.ToString())
                );

                if (!result.Succeeded)
                {
                    string? errorMessage = result.Errors.FirstOrDefault()?.Description;
                    logger.LogError(errorMessage);
                    return errorMessage;
                }
            }

        return null;
    }
}