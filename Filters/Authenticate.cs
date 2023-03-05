using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Filters;

public class Authenticate : ActionFilterAttribute
{
    private readonly FulcrumContext _context;

    public Authenticate(FulcrumContext context)
    {
        _context = context;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var currentToken = context.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "Token").Value;

        if (! _context.Sessions.AsNoTracking()
            .Any(x => x.Token == currentToken && x.Expires > DateTime.UtcNow))
        {
            context.Result = new StatusCodeResult(401);
        }
    }
}