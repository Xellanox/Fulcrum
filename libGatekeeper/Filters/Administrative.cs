using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace libGatekeeper.Filters;

public class Administrative : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var currentToken = context.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "Token").Value;

        using var db = context.HttpContext.RequestServices.GetService<GatekeeperContext>();

        if (!db.Sessions.AsNoTracking().Any(x => x.Token == currentToken && x.Expires > DateTime.UtcNow))
        {
            context.Result = new StatusCodeResult(401);
        }
        else
        {
            var currentSession = db.Sessions.AsNoTracking().FirstOrDefault(x => x.Token == currentToken && x.Expires > DateTime.UtcNow);

            if (!db.Users.AsNoTracking().Any(x => x.UserId == currentSession.UserId && x.IsAdmin))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}