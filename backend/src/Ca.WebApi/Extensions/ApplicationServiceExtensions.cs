using Ca.Shared.Configurations.Common.SeedSettings;

namespace Ca.WebApi.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddAppAdminSeederConfigsService(
        this IServiceCollection services, IConfiguration config
    )
    {
        services.Configure<AppAdminSeedInfo>(
            config.GetSection(nameof(AppAdminSeedInfo))
        );

        services.AddOptions<AppAdminSeedInfo>().Bind(config.GetSection(nameof(AppAdminSeedInfo))).Validate(
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