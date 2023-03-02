using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace libGatekeeper;

public static class MigrationManager
{
    public static WebApplication MigrateGatekeeper(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<GatekeeperContext>();

        if (!context.Database.CanConnect())
        {
            context.Database.Migrate();
        }
        
        

        return app;
    }
}