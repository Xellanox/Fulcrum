using Microsoft.EntityFrameworkCore;

namespace Fulcrum;

public static class MigrationManager
{
    public static WebApplication MigrateFulcrum(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();

        var context = serviceScope.ServiceProvider.GetService<FulcrumContext>();

        if (!context.Database.CanConnect())
        {
            context.Database.Migrate();
        }

        return app;
    }
}