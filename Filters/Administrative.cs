using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Filters;

public class Administrative : ActionFilterAttribute
{
    private readonly FulcrumContext _context;

    public Administrative(FulcrumContext context)
    {
        _context = context;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var currentToken = context.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "Token").Value;

        if (! _context.Sessions.AsNoTracking()
            .Include(x => x.UserDetails)
            .Any(x => x.Token == currentToken && x.Expires > DateTime.UtcNow && x.UserDetails.IsAdmin))
        {
            context.Result = new StatusCodeResult(401);
        }
    }
}