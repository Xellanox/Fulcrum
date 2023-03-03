using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace libFulcrum;

public static class FulcrumServiceGroup
{
    public static IServiceCollection AddFulcrum(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<FulcrumContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("FulcrumDB"), b => b.MigrationsAssembly("libFulcrum")));
            
        return services;
    }
}