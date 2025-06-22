using Ca.Shared.Configurations.Common.SeedSettings;

namespace Ca.WebApi.Extensions;

internal static class ApplicationServiceExtensions
{
    internal static IServiceCollection AddAppAdminSeederConfigsService(
        this IServiceCollection services, IConfiguration config
    )
    {
        services.Configure<SuperAdminSeedInfo>(
            config.GetSection(nameof(SuperAdminSeedInfo))
        );

        services.AddOptions<SuperAdminSeedInfo>().Bind(config.GetSection(nameof(SuperAdminSeedInfo))).Validate(
            info => !string.IsNullOrWhiteSpace(info.FirstName) &&
                    !string.IsNullOrWhiteSpace(info.LastName) &&
                    !string.IsNullOrWhiteSpace(info.Email) &&
                    !string.IsNullOrWhiteSpace(info.Password)
            ,
            "All appAdminSeedInfo fields are required."
        ).ValidateOnStart(); // Fail fast at startup

        return services;
    }
}