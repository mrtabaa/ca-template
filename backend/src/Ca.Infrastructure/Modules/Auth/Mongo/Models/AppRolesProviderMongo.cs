using Ca.Domain.Modules.Auth.Enums;

namespace Ca.Infrastructure.Modules.Auth.Mongo.Models;

public static class AppRolesProviderMongo
{
    public static readonly AppRoleMongo[] AppRoles =
    [
        new() { Name = GetRoleStrValue(role: Role.Admin) },
        new() { Name = GetRoleStrValue(role: Role.Moderator) },
        new() { Name = GetRoleStrValue(role: Role.Member) }
    ];

    public static string GetRoleStrValue(Role role) =>
        role.ToString().ToUpper();
}