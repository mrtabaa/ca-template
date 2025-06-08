using Ca.Shared.Configurations.Mongo.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ca.Infrastructure.Persistence.Mongo.Extensions;

public static class ServiceExtensionsMongo
{
    public static IServiceCollection AddServiceMongo(this IServiceCollection services)
    {
        // get values
        services.AddSingleton<IMyMongoDbSettings>(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<MyMongoDbSettings>>().Value
        );

        // get connectionString to the db
        services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                MyMongoDbSettings myMongoDbSettings = serviceProvider.GetRequiredService<IOptions<MyMongoDbSettings>>().
                    Value;

                return new MongoClient(myMongoDbSettings.ConnectionString);
            }
        );

        return services;
    }
}