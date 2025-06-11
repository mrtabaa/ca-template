using Ca.Domain.Modules.AccessControl;
using Ca.Domain.Modules.AccessControl.Aggregates;
using Ca.Domain.Modules.AccessControl.Enums;
using Ca.Domain.Modules.AccessControl.ValueObjects;
using Ca.Shared.Configurations.Common.SeedSettings;
using Microsoft.Extensions.Options;

namespace Ca.Application.Modules.AccessControl;

public class AccessControlService(
    IAccessControlRepository accessControlRepository,
    IOptions<SuperAdminSeedInfo> appAdminSeedInfo
) : IAccessControlService
{
    /// <summary>
    ///     Assign users' role and permissions.
    /// </summary>
    /// <param name="roleName"></param>
    /// <param name="permissions"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task AssignRoleAndPermissionsAsync(string roleName, IEnumerable<Permission> permissions) =>
        throw new NotImplementedException();

    /// <summary>
    ///     Assigns the SuperAdmin role and permissions.
    /// </summary>
    /// <param name="roleName"></param>
    public async Task SeedSuperAdminRoleAndPermissionsAsync(string roleName)
    {
        var appRole = AppRole.CreateSuperAdmin(roleName, AccessPermissionType.AccessSuperAdminPanel);

        await accessControlRepository.CreateAppRoleAsync(appRole);
    }
}