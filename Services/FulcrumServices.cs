using Fulcrum.Services.Authentication;
using Fulcrum.Services.CurrentUser;
using Fulcrum.Services.UserManagement;
using Microsoft.EntityFrameworkCore;
using Fulcrum.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using Fulcrum.Services.CurrentConfig;

namespace Fulcrum.Services;

public static class FulcrumServices
{
    public static IServiceCollection AddFulcrum(this IServiceCollection services, IConfiguration configuration)
    {
        if (Environment.GetEnvironmentVariable("FULCRUM_DB") != null)
        {
            services.AddDbContext<FulcrumContext>(x => 
                x.UseSqlServer(Environment.GetEnvironmentVariable("FULCRUM_DB")));
        }
        else
        {
            services.AddDbContext<FulcrumContext>(x => 
                x.UseSqlServer(configuration.GetConnectionString("default")));
        }

        services.AddScoped<IAuthentication, AuthenticationService>();
        services.AddScoped<IUserManagement, UserManagementService>();
        services.AddScoped<ICurrentUser, CurrentUserService>();
        services.AddScoped<ICurrentConfig, CurrentConfigService>();

        services.AddScoped<ActionFilterAttribute, Administrative>();
        services.AddScoped<ActionFilterAttribute, Authenticate>();

        return services;
    }
}