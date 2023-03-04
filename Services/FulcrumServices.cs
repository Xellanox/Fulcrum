using Fulcrum.Services.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Services;

public static class FulcrumServices
{
    public static IServiceCollection AddFulcrum(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FulcrumContext>(x => 
            x.UseSqlServer(Environment.GetEnvironmentVariable("FULCRUM_DB"))
        );

        services.AddScoped<IAuthentication, AuthenticationService>();

        return services;
    }
}