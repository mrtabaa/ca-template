using Ca.Shared.Configurations.Mongo.Settings;

namespace Ca.WebApi.Extensions;

internal static class ServiceExtensionsMongo
{
    internal static IServiceCollection AddConfigsServiceMongo(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddOptions<MyMongoDbSettings>()
            .Bind(config.GetSection(nameof(MyMongoDbSettings)))
            .Validate(settings =>
                    !string.IsNullOrWhiteSpace(settings.ConnectionString) &&
                    !string.IsNullOrWhiteSpace(settings.DatabaseName),
                "MongoDB ConnectionString and DatabaseName are required.")
            .ValidateOnStart(); // Fail fast at startup

        return services;
    }
}