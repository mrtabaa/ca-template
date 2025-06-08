using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Ca.Infrastructure.Modules.Auth.Mongo.Models;
using Ca.Shared.Configurations.Mongo.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace Ca.Infrastructure.Persistence.Mongo.Extensions;

public static class IdentityExtensionsMongo
{
    public static IServiceCollection AddIdentityServiceMongo(this IServiceCollection services)
    {
        services.AddSingleton<MongoDbIdentityConfiguration>(provider =>
        {
            MyMongoDbSettings myMongoDbSettings = provider.GetRequiredService<IOptions<MyMongoDbSettings>>().Value;
            
            return new MongoDbIdentityConfiguration
            {
                MongoDbSettings = new MongoDbSettings
                {
                    ConnectionString = myMongoDbSettings.ConnectionString,
                    DatabaseName = myMongoDbSettings.DatabaseName
                },
                IdentityOptionsAction = options =>
                {
                    // Unique email
                    options.User.RequireUniqueEmail = true;

                    // Require confirmed email but no account confirmation
                    options.SignIn.RequireConfirmedEmail = true;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;

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
        });

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        var mongoConfig = serviceProvider.GetRequiredService<MongoDbIdentityConfiguration>();

        services.ConfigureMongoDbIdentity<AppUserMongo, AppRoleMongo, ObjectId>(mongoConfig)
            .AddUserManager<UserManager<AppUserMongo>>()
            .AddSignInManager<SignInManager<AppUserMongo>>()
            .AddRoleManager<RoleManager<AppRoleMongo>>()
            .AddDefaultTokenProviders();

        return services;
    }
}