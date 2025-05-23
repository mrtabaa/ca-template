using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace Ca.Infrastructure.Modules.Auth.Mongo.Models;

[CollectionName("users")]
public class AppUserMongo : MongoIdentityUser<ObjectId>
{
    public string Name { get; init; } = string.Empty;
    public bool IsAlive { get; init; }
}