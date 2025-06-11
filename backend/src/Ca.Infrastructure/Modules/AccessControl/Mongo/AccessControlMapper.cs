using Ca.Domain.Modules.AccessControl.Aggregates;
using Ca.Infrastructure.Modules.AccessControl.Mongo.Models;

namespace Ca.Infrastructure.Modules.AccessControl.Mongo;

public static class AccessControlMapper
{
    internal static AppRoleMongo MapAppUserToMongoAppUser(AppRole appUser) =>
        new()
        {
            Name = appUser.Name.Value,
            Permissions = appUser.Permissions,
            IsLocked = appUser.IsLocked
        };
}