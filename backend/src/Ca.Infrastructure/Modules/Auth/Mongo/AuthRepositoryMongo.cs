using Ca.Domain.Modules.Auth;
using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Domain.Modules.Auth.Enums;
using Ca.Domain.Modules.Auth.Results;
using Ca.Infrastructure.Modules.Auth.Mongo.Models;
using Ca.Infrastructure.Modules.Common.Mongo;
using Ca.Shared.Configurations.Mongo.Settings;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace Ca.Infrastructure.Modules.Auth.Mongo;

public class AuthRepositoryMongo : IAuthRepository
{
    #region CRUD

    public async Task<AuthCreationResponse> CreateAsync(AppUser appUser, CancellationToken cancellationToken)
    {
        AppUserMongo? existingUser = await _userManager.FindByEmailAsync(appUser.Email.Value);
        if (existingUser != null)
        {
            return new AuthCreationResponse(
                AppUser: null // Enum's default value
            );
        }


        AppUserMongo appUserMongo = CommonMapperMongo.MapAppUserToAppUserMongo(appUser);

        IdentityResult userCreatedResult = await _userManager.CreateAsync(
            appUserMongo, appUser.Password
        );
        if (!userCreatedResult.Succeeded)
        {
            List<string> errors = userCreatedResult.Errors.Select(e => e.Description).ToList();

            if (errors.Any(e => e.Contains(
                        "is already taken", StringComparison.OrdinalIgnoreCase
                    )
                ))
            {
                if (errors.Any(e => e.Contains("email", StringComparison.OrdinalIgnoreCase)
                    ))
                {
                    return new AuthCreationResponse(
                        AppUser: null
                    );
                }

                if (errors.Any(e => e.Contains(
                            "username", StringComparison.OrdinalIgnoreCase
                        )
                    ))
                {
                    return new AuthCreationResponse(
                        AppUser: null,
                        AuthCreationResult.UsernameAlreadyExists
                    );
                }
            }

            return new AuthCreationResponse(AppUser: null);
        }

        IdentityResult roleResult = await _userManager.AddToRoleAsync(
            appUserMongo, AppRolesProviderMongo.GetRoleStrValue(Role.Client)
        );
        if (!roleResult.Succeeded) // Failed to add the role. Delete appUser from DB
        {
            await _userManager.DeleteAsync(appUserMongo);
            return new AuthCreationResponse(AppUser: null);
        }

        // Account created successfully.
        return new AuthCreationResponse(appUser);
    }

    #endregion CRUD

    #region Db and Token Settings

    private readonly IMongoCollection<AppUserMongo> _collectionUsers;
    private readonly IMongoCollection<RefreshTokenMongo> _collectionRefreshTokens;

    // private readonly IRecaptchaService _recaptchaService; // TODO: Move it
    private readonly UserManager<AppUserMongo> _userManager;

    // constructor - dependency injection
    public AuthRepositoryMongo(
        IMongoClient client, IMyMongoDbSettings dbSettings,
        UserManager<AppUserMongo> userManager
    )
    {
        IMongoDatabase dbName = client.GetDatabase(dbSettings.DatabaseName);
        _collectionUsers = dbName.GetCollection<AppUserMongo>(CollectionNamesMongo.Users);
        _collectionRefreshTokens = dbName.GetCollection<RefreshTokenMongo>(CollectionNamesMongo.RefreshTokens);
        _userManager = userManager;
    }

    #endregion
}