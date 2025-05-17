using Ca.Domain.Modules.Auth.Enums;
using Ca.Infrastructure.Modules.Auth.Mongo.Models;

namespace Ca.Infrastructure.Modules.Auth;

public static class AppRolesProvider
{
    public static readonly MongoAppRole[] AppRoles =
    [
        new() { Name = GetRoleStrValue(Roles.Admin) },
        new() { Name = GetRoleStrValue(Roles.Moderator) },
        new() { Name = GetRoleStrValue(Roles.Member) }
    ];

    public static string GetRoleStrValue(Roles role) =>
        role.ToString().ToUpper();
}