namespace Ca.WebApi.Extensions;

public static class PolicyServiceExtensions
{
    public static IServiceCollection AddPolicyService(this IServiceCollection services) =>
        // Add authorization policies for roles
        // services.AddAuthorizationBuilder().AddPolicy(
        //     name: AppVariablesExtensions.RequiredAdminRole,
        //     configurePolicy: policy => policy.RequireRole(EnumExtensions.GetRoleStrValue(Role.SuperAdmin))
        // ).AddPolicy(
        //     name: AppVariablesExtensions.RequiredModeratorRole,
        //     configurePolicy: policy => policy.RequireRole(
        //         EnumExtensions.GetRoleStrValue(Role.SuperAdmin), EnumExtensions.GetRoleStrValue(Role.Moderator)
        //     )
        // );
        services;
}