using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Infrastructure.Modules.Auth.Mongo.Models;

namespace Ca.Infrastructure.Modules.Common.Mongo;

public static class CommonMapperMongo
{
    internal static AppUserMongo MapAppUserToAppUserMongo(AppUser appUser) =>
        new()
        {
            Name = appUser.Name,
            Email = appUser.Email,
            IsAlive = appUser.IsAlive
        };

    internal static AppUser MapMongoAppUserToAppUser(AppUserMongo appUserMongo) =>
        new(appUserMongo.Name, appUserMongo.Email, appUserMongo.IsAlive);
}