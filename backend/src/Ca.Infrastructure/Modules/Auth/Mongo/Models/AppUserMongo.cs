using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace Ca.Infrastructure.Modules.Auth.Mongo.Models;

[CollectionName("users")]
public class AppUserMongo : MongoIdentityUser<ObjectId>
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}