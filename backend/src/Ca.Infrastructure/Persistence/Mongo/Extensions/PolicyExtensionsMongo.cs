using Ca.Domain.Modules.Common.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace Ca.Infrastructure.Persistence.Mongo.Extensions;

public static class PolicyExtensionsMongo
{
    //TODO:
    // public static IServiceCollection AddPolicyService(this IServiceCollection services)
    // {
    //     // Add authorization policies for roles
    //     services.AddAuthorizationBuilder()
    //         .AddPolicy(
    //             AppVariablesExtensions.RequiredAdminRole,
    //             policy => policy.RequireRole(EnumExtensions.GetRoleStrValue(Roles.Admin))
    //         )
    //         .AddPolicy(
    //             AppVariablesExtensions.RequiredModeratorRole,
    //             policy => policy.RequireRole(
    //                 EnumExtensions.GetRoleStrValue(Roles.Admin), EnumExtensions.GetRoleStrValue(Roles.Moderator)
    //             )
    //         );
    //
    //     return services;
    // }

}