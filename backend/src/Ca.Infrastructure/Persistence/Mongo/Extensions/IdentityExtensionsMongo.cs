using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Infrastructure.Persistence.Mongo.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace Ca.Infrastructure.Persistence.Mongo.Extensions;

public static class IdentityExtensionsMongo
{
    public static IServiceCollection AddMongoIdentityService(this IServiceCollection services)
    {
        // Use DI to fetch MyMongoDbSettings from IOptions
        services.AddSingleton(provider =>
            {
                // Resolve MyMongoDbSettings using DI 
                var mongoDbSettings = provider.GetRequiredService<IOptions<MyMongoDbSettings>>().Value;

                return new MongoDbIdentityConfiguration
                {
                    MongoDbSettings = new MongoDbSettings
                    {
                        ConnectionString = mongoDbSettings.ConnectionString,
                        DatabaseName = mongoDbSettings.DatabaseName
                    },
                    IdentityOptionsAction = options =>
                    {
                        // Unique email
                        options.User.RequireUniqueEmail = true;

                        // Require confirmed email but no account confirmation
                        options.SignIn.RequireConfirmedEmail = true;
                        options.SignIn.RequireConfirmedAccount = false;
                        options.Tokens.EmailConfirmationTokenProvider =
                            TokenOptions.DefaultEmailProvider; // shorten code to 6 digits

                        // Token handling
                        options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;

                        // Password requirements
                        options.Password.RequireDigit = true;
                        options.Password.RequireUppercase = true;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequiredLength = 8;

                        // Lockout configuration
                        options.Lockout.AllowedForNewUsers = true;
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                        options.Lockout.MaxFailedAccessAttempts = 5;
                    }
                };
            }
        );

        // Register Identity services configured via MongoDbIdentityConfiguration
        // TODO:
        // services.ConfigureMongoDbIdentity<AppUser, AppRole, ObjectId>(
        //         provider.GetRequiredService<MongoDbIdentityConfiguration>()
        //     ).AddUserManager<UserManager<AppUser>>().AddSignInManager<SignInManager<AppUser>>().
        //     AddRoleManager<RoleManager<AppRole>>().AddDefaultTokenProviders();

        return services;
    }
}