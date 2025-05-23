using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Infrastructure.Modules.Auth.Mongo.Models;

namespace Ca.Infrastructure.Modules.Payment.Mongo;

internal static class PaymentMapperMongo
{
    internal static AppUserMongo MapAppUserToMongoAppUser(AppUser appUser) =>
        new();
}