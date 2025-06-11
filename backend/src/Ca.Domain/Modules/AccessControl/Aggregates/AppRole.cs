using Ca.Domain.Modules.AccessControl.Enums;
using Ca.Domain.Modules.AccessControl.ValueObjects;
using Ca.Domain.Modules.Auth.ValueObjects;
using Ca.Domain.Modules.Common.Exceptions;

namespace Ca.Domain.Modules.AccessControl.Aggregates;

public class AppRole
{
    private AppRole()
    {
    }

    public Name Name { get; private set; }
    public HashSet<Permission> Permissions { get; private set; } = [];
    public bool IsLocked { get; private set; } // Prevent from changing permissions. SuperAdmin role is always locked.

    public static AppRole CreateSuperAdmin(
        string superAdminRoleName, AccessPermissionType accessPermissionType
    ) // First seed
    {
        if (accessPermissionType != AccessPermissionType.AccessSuperAdminPanel)
        {
            throw new DomainException(
                "Only creating SuperAdmin role with AccessSuperAdminPanel permission is allowed."
            );
        }

        return new AppRole
        {
            Name = Name.Create(superAdminRoleName),
            Permissions = [Permission.CreateUnsafe(AccessPermissionType.AccessSuperAdminPanel)],
            IsLocked = true
        };
    }

    public static AppRole Create(string nameRaw, IEnumerable<Permission> permissionsRaw, bool isLocked = false)
    {
        var name = Name.Create(nameRaw);

        HashSet<Permission> allowedPermissions = []; // Keep permissions unique by HashSet
        foreach (Permission permissionRaw in permissionsRaw)
            allowedPermissions.Add(Permission.Create(permissionRaw.Value)); // Prevent from registering a SuperAdmin

        return new AppRole
        {
            Name = name,
            Permissions = allowedPermissions,
            IsLocked = isLocked
        };
    }

    public void AddPermission(AccessPermissionType accessPermissionType)
    {
        if (IsLocked)
            throw new DomainException("Cannot modify permissions of a locked role.");

        Permissions.Add(Permission.Create(accessPermissionType));
    }

    public void RemovePermission(AccessPermissionType accessPermissionType)
    {
        if (IsLocked)
            throw new DomainException("Cannot modify permissions of a locked role.");

        Permissions.Remove(Permission.Create(accessPermissionType));
    }

    public void Lock() => IsLocked = true;

    public void Unlock()
    {
        if (Permissions.Any(p => p.IsAccessSuperAdminPanel))
            throw new DomainException("Cannot unlock the SuperAdmin role.");

        IsLocked = false;
    }
}