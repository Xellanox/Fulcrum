using Fulcrum.Services.Authentication;
using Fulcrum.Services.UserManagement;
using Microsoft.EntityFrameworkCore;

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

        return services;
    }
}