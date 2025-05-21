using Ca.Domain.Modules.Auth;
using Ca.Domain.Modules.Auth.Entities;
using Ca.Domain.Modules.Auth.Enums;
using Ca.Domain.Modules.Auth.Results;
using Ca.Infrastructure.Modules.Auth.Mongo.Models;
using Ca.Infrastructure.Modules.Common.Mongo;
using Ca.Infrastructure.Persistence.Mongo.Settings;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace Ca.Infrastructure.Modules.Auth.Mongo;

public class MongoAuthRepository : IAuthRepository
{
    #region Db and Token Settings

    private readonly IMongoCollection<MongoAppUser> _collectionUsers;
    private readonly IMongoCollection<MongoRefreshToken> _collectionRefreshTokens;
    
    // private readonly IRecaptchaService _recaptchaService; // TODO: Move it
    private readonly UserManager<MongoAppUser> _userManager;

    // constructor - dependency injection
    public MongoAuthRepository(
        IMongoClient client, IMyMongoDbSettings dbSettings,
        UserManager<MongoAppUser> userManager
    )
    {
        IMongoDatabase dbName = client.GetDatabase(dbSettings.DatabaseName)
                                ?? throw new ArgumentNullException(nameof(dbName), "The database name cannot be null.");
        _collectionUsers = dbName.GetCollection<MongoAppUser>(MongoCollectionNames.Users);
        _collectionRefreshTokens = dbName.GetCollection<MongoRefreshToken>(MongoCollectionNames.RefreshTokens);
        _userManager = userManager;
    }

    #endregion

    #region CRUD

    public async Task<AuthCreationResponse> CreateAsync(AppUser appUser, CancellationToken cancellationToken)
    {
        MongoAppUser? existingUser = await _userManager.FindByEmailAsync(appUser.Email);
        if (existingUser != null)
            return new AuthCreationResponse(
                AppUser: null,
                AuthCreationResult.EmailAlreadyExists // Enum's default value
            );
        
        MongoAppUser mongoAppUser = MongoAuthMapper.MapAppUserToMongoAppUser(appUser);

        IdentityResult userCreatedResult = await _userManager.CreateAsync(mongoAppUser, appUser.Password);
        if (!userCreatedResult.Succeeded)
        {
            List<string> errors = userCreatedResult.Errors.Select(e => e.Description).ToList();

            if (errors.Any(e => e.Contains("is already taken", StringComparison.OrdinalIgnoreCase)))
            {
                if (errors.Any(e => e.Contains("email", StringComparison.OrdinalIgnoreCase)))
                    return new AuthCreationResponse(
                        AppUser: null,
                        AuthCreationResult.EmailAlreadyExists
                    );

                if (errors.Any(e => e.Contains("username", StringComparison.OrdinalIgnoreCase)))
                    return new AuthCreationResponse(
                        AppUser: null,
                        AuthCreationResult.UsernameAlreadyExists
                    );
            }

            return new AuthCreationResponse(AppUser: null);        
        }

        IdentityResult roleResult = await _userManager.AddToRoleAsync(
            mongoAppUser, AppRolesProvider.GetRoleStrValue(Roles.Member)
        );
        if (!roleResult.Succeeded) // Failed to add the role. Delete appUser from DB
        {
            await _userManager.DeleteAsync(mongoAppUser);
            return new AuthCreationResponse(AppUser: null);        }

        // Account created successfully.
        return new AuthCreationResponse(AppUser: appUser); 
    }
    
    #endregion CRUD
}