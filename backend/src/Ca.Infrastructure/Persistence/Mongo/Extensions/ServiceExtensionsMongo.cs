using Ca.Infrastructure.Persistence.Mongo.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ca.Infrastructure.Persistence.Mongo.Extensions;

public static class ServiceExtensionsMongo
{
    public static IServiceCollection AddMongoDbSettingsService(this IServiceCollection services, IConfiguration config)
    {
        IConfigurationSection section = config.GetSection(nameof(MyMongoDbSettings));
    
        services.Configure<MyMongoDbSettings>(settings =>
        {
            settings.ConnectionString = section[nameof(MyMongoDbSettings.ConnectionString)]
                                        ?? throw new InvalidOperationException("MongoDB ConnectionString is missing.");

            settings.DatabaseName = section[nameof(MyMongoDbSettings.DatabaseName)]
                                    ?? throw new InvalidOperationException("MongoDB DatabaseName is missing.");
        });

        return services;

    }
}