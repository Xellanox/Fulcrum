using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace libGatekeeper.Filters;

public class Administrative : ActionFilterAttribute
{
    private readonly GatekeeperContext _context;

    public Administrative(GatekeeperContext context)
    {
        _context = context;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var currentToken = context.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "Token").Value;

        if (!_context.Sessions.AsNoTracking().Any(x => x.Token == currentToken && x.Expires > DateTime.UtcNow))
        {
            context.Result = new StatusCodeResult(401);
        }
        else
        {
            var currentSession = _context.Sessions.AsNoTracking().FirstOrDefault(x => x.Token == currentToken && x.Expires > DateTime.UtcNow);

            if (!_context.Users.AsNoTracking().Any(x => x.UserId == currentSession.UserId && x.IsAdmin))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}