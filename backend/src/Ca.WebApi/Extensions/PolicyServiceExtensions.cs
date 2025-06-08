using Ca.Domain.Modules.Common.Enums;

namespace Ca.WebApi.Extensions;

public static class PolicyServiceExtensions
{
    public static IServiceCollection AddPolicyService(this IServiceCollection services)
    {
        // // Add authorization policies for roles
        // services.AddAuthorizationBuilder()
        //     .AddPolicy(
        //         AppVariablesExtensions.RequiredAdminRole,
        //         policy => policy.RequireRole(EnumExtensions.GetRoleStrValue(Roles.Admin))
        //     )
        //     .AddPolicy(
        //         AppVariablesExtensions.RequiredModeratorRole,
        //         policy => policy.RequireRole(
        //             EnumExtensions.GetRoleStrValue(Roles.Admin), EnumExtensions.GetRoleStrValue(Roles.Moderator)
        //         )
        //     );
    
        return services;
    }

}