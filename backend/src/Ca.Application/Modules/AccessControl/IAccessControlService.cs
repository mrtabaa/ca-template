using Ca.Domain.Modules.AccessControl.ValueObjects;

namespace Ca.Application.Modules.AccessControl;

public interface IAccessControlService
{
    public Task AssignRoleAndPermissionsAsync(string roleName, IEnumerable<Permission> permissions);
    public Task SeedSuperAdminRoleAndPermissionsAsync(string roleName);
}