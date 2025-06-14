using Ca.Domain.Modules.AccessControl.Enums;
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

    /// <summary>
    ///     Create a new user.
    /// </summary>
    /// <param name="appUser"></param>
    /// <param name="roleType"></param>
    /// <returns>AuthUserCreationResult</returns>
    public async Task<AuthUserCreationResult> CreateAppUserAsync(
        AppUser appUser, AccessRoleType roleType
    ) =>
        await ImplementCreateUserAppAsync(appUser, roleType);

    /// <summary>
    ///     Seed SuperAdmin AppUser.
    /// </summary>
    /// <param name="appUser"></param>
    /// <param name="roleType"></param>
    /// <returns>AuthUserCreationResult</returns>
    public async Task<AuthUserCreationResult> SeedSuperAdminAppUserAsync(AppUser appUser, AccessRoleType roleType) =>
        await ImplementCreateUserAppAsync(appUser, roleType);

    /// <summary>
    ///     The implementation of CreateAppUserAsync.
    /// </summary>
    /// <param name="appUser"></param>
    /// <param name="roleType"></param>
    /// <returns></returns>
    private async Task<AuthUserCreationResult> ImplementCreateUserAppAsync(AppUser appUser, AccessRoleType roleType)
    {
        // Check before creation for performance since username/email are checked in _userManager.CreateAppUserAsync 
        AppUserMongo? existingUser = await _userManager.FindByEmailAsync(appUser.Email?.Value);
        if (existingUser != null)
        {
            return new AuthUserCreationResult(
                AppUser: null,
                AuthUserCreationStatus.EmailAlreadyExists
            );
        }

        AppUserMongo appUserMongo = CommonMapperMongo.MapAppUserToAppUserMongo(appUser);

        IdentityResult userCreatedResult = await _userManager.CreateAsync(
            appUserMongo, appUser.Password?.Value
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
                    return new AuthUserCreationResult(
                        AppUser: null
                    );
                }

                if (errors.Any(e => e.Contains(
                            "username", StringComparison.OrdinalIgnoreCase
                        )
                    ))
                {
                    return new AuthUserCreationResult(
                        AppUser: null,
                        AuthUserCreationStatus.UsernameAlreadyExists
                    );
                }
            }

            return new AuthUserCreationResult(AppUser: null);
        }

        IdentityResult roleResult = await _userManager.AddToRoleAsync(
            appUserMongo, roleType.ToString()
        );
        if (!roleResult.Succeeded) // Failed to add the role. Delete appUser from DB
        {
            await _userManager.DeleteAsync(appUserMongo);
            return new AuthUserCreationResult(AppUser: null);
        }

        // Account created successfully.
        return new AuthUserCreationResult(appUser);
    }

    #endregion CRUD

    #region Db Settings

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