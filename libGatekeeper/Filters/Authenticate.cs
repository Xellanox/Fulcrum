using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace libGatekeeper.Filters;

public class Authenticate : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var currentToken = context.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "Token").Value;

        var db = context.HttpContext.RequestServices.GetService<GatekeeperContext>();

        if (!db.Sessions.AsNoTracking().Any(x => x.Token == currentToken && x.Expires > DateTime.UtcNow))
        {
            context.Result = new StatusCodeResult(401);
        }
    }
}