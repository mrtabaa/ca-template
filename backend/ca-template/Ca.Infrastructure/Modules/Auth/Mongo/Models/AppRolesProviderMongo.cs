using Ca.Domain.Modules.Shared.Enums;

namespace Ca.Infrastructure.Modules.Auth.Mongo.Models;

public static class AppRolesProviderMongo
{
    public static readonly AppRoleMongo[] AppRoles =
    [
        new() { Name = GetRoleStrValue(Roles.Admin) },
        new() { Name = GetRoleStrValue(Roles.Moderator) },
        new() { Name = GetRoleStrValue(Roles.Member) }
    ];

    public static string GetRoleStrValue(Roles role) =>
        role.ToString().ToUpper();
}