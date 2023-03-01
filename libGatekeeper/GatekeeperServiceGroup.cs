using System.Globalization;
using libGatekeeper.Services.Authentication;
using libGatekeeper.Services.UserManagement;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class GatekeeperServiceGroup
{
    public static IServiceCollection AddGatekeeper(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserManagementService, UserManagementService>();

        return services;
    }
}