using System.Globalization;
using libGatekeeper.Services.Authentication;
using libGatekeeper.Services.UserManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using libGatekeeper;
using Microsoft.EntityFrameworkCore;
using libGatekeeper.Services.UserAdministration;
using libGatekeeper.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace libGatekeeper;

public static class GatekeeperServiceGroup
{
    public static IServiceCollection AddGatekeeper(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<GatekeeperContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("GatekeeperDB"), b => b.MigrationsAssembly("libGatekeeper")));
        
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserManagementService, UserManagementService>();
        services.AddScoped<IUserAdministrationService, UserAdministrationService>();
        services.AddScoped<ActionFilterAttribute, Administrative>();
        services.AddScoped<ActionFilterAttribute, Authenticate>();

        return services;
    }
}