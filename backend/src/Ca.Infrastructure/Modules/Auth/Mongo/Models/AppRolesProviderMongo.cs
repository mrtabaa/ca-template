using Ca.Domain.Modules.Auth.Enums;

namespace Ca.Infrastructure.Modules.Auth.Mongo.Models;

public static class AppRolesProviderMongo
{
    public static readonly AppRoleMongo[] AppRoles =
    [
        new() { Name = GetRoleStrValue(Role.AppAdmin) },
        new() { Name = GetRoleStrValue(Role.Moderator) },
        new() { Name = GetRoleStrValue(Role.Client) }
    ];

    public static string GetRoleStrValue(Role role) =>
        role.ToString().ToUpper();
}