using AspNetCore.Identity.MongoDbCore.Models;
using Ca.Domain.Modules.AccessControl.Enums;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace Ca.Infrastructure.Modules.AccessControl.Mongo.Models;

[CollectionName("roles")]
public class AppRoleMongo : MongoIdentityRole<ObjectId>
{
    public IEnumerable<AccessPermissionType> Permissions { get; init; } = [];
    public bool IsLocked { get; init; }
}