using Ca.Application.Modules.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Ca.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        
        return services;
    }
}