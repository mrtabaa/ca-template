using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace Ca.Infrastructure.Modules.Auth.Mongo.Models;

[CollectionName("roles")]
public class AppRoleMongo : MongoIdentityRole<ObjectId>
{
}