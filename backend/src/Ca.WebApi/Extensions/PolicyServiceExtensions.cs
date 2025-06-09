using Ca.Domain.Modules.Auth.Enums;
using Microsoft.OpenApi.Extensions;

namespace Ca.WebApi.Extensions;

public static class PolicyServiceExtensions
{
    public static IServiceCollection AddPolicyService(this IServiceCollection services)
    {
        // Add authorization policies for roles
        services.AddAuthorizationBuilder().AddPolicy(
            name: AppVariablesExtensions.RequiredAdminRole,
            configurePolicy: policy => policy.RequireRole(EnumExtensions.GetRoleStrValue(Role.Admin))
        ).AddPolicy(
            name: AppVariablesExtensions.RequiredModeratorRole,
            configurePolicy: policy => policy.RequireRole(
                EnumExtensions.GetRoleStrValue(Role.Admin), EnumExtensions.GetRoleStrValue(Role.Moderator)
            )
        );

        return services;
    }
}