using Ca.Domain.Modules.Auth.Entities;
using Ca.Domain.Modules.Auth.Enums;

namespace Ca.Domain.Modules.Auth.Constants;

public static class AppPermissionsMetadata
{
    public static readonly List<AppPermission> All =
    [
        new() { Permission = Permission.BanUser, Description = "Allows banning users" },
        new() { Permission = Permission.DeleteUser, Description = "Allows deleting user accounts" },
        new() { Permission = Permission.AccessAdminPanel, Description = "Allows access to admin interface" },
        new() { Permission = Permission.AccessParkingPanel, Description = "Allows access to parking interface" },
        new() { Permission = Permission.EditOwnProfile, Description = "Edit your own profile" },
        new() { Permission = Permission.ApprovePhotos, Description = "Approve or reject uploaded photos" },
        new() { Permission = Permission.SendMessages, Description = "Send private messages" }
    ];
}