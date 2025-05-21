using Ca.Domain.Modules.Auth;
using Ca.Domain.Modules.Auth.Aggregates;
using Ca.Domain.Modules.Auth.Enums;
using Ca.Domain.Modules.Auth.Results;
using Ca.Domain.Modules.Shared.Enums;
using Ca.Infrastructure.Modules.Auth.Mongo.Models;
using Ca.Infrastructure.Modules.Common.Mongo;
using Ca.Infrastructure.Persistence.Mongo.Settings;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace Ca.Infrastructure.Modules.Auth.Mongo;

public class AuthRepositoryMongo : IAuthRepository
{
    #region CRUD

    public async Task<AuthCreationResponse> CreateAsync(AppUser appUser, CancellationToken cancellationToken)
    {
        AppUserMongo? existingUser = await _userManager.FindByEmailAsync(appUser.Email);
        if (existingUser != null)
        {
            return new AuthCreationResponse(
                null // Enum's default value
            );
        }

        AppUserMongo appUserMongo = CommonMapperMongo.MapAppUserToAppUserMongo(appUser);

        IdentityResult userCreatedResult = await _userManager.CreateAsync(appUserMongo, appUser.Password);
        if (!userCreatedResult.Succeeded)
        {
            List<string> errors = userCreatedResult.Errors.Select(e => e.Description).ToList();

            if (errors.Any(e => e.Contains("is already taken", StringComparison.OrdinalIgnoreCase)))
            {
                if (errors.Any(e => e.Contains("email", StringComparison.OrdinalIgnoreCase)))
                {
                    return new AuthCreationResponse(
                        null
                    );
                }

                if (errors.Any(e => e.Contains("username", StringComparison.OrdinalIgnoreCase)))
                {
                    return new AuthCreationResponse(
                        null,
                        AuthCreationResult.UsernameAlreadyExists
                    );
                }
            }

            return new AuthCreationResponse(null);
        }

        IdentityResult roleResult = await _userManager.AddToRoleAsync(
            appUserMongo, AppRolesProvider.GetRoleStrValue(Roles.Member)
        );
        if (!roleResult.Succeeded) // Failed to add the role. Delete appUser from DB
        {
            await _userManager.DeleteAsync(appUserMongo);
            return new AuthCreationResponse(null);
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
        IMongoDatabase dbName = client.GetDatabase(dbSettings.DatabaseName)
                                ?? throw new ArgumentNullException(nameof(dbName), "The database name cannot be null.");
        _collectionUsers = dbName.GetCollection<AppUserMongo>(CollectionNamesMongo.Users);
        _collectionRefreshTokens = dbName.GetCollection<RefreshTokenMongo>(CollectionNamesMongo.RefreshTokens);
        _userManager = userManager;
    }

    #endregion
}