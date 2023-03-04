using System.Reflection.Metadata.Ecma335;
using Azure.Core;
using Fulcrum.Services.Authentication.Types;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Services.Authentication;

internal partial class AuthenticationService : IAuthentication
{
    public async Task<Logout.LogoutResponse> Logout()
    {
        var token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];

        if (token == "" || token == null)
        {
            return new Logout.LogoutResponse
            {
                Message = "Logout failed, no valid session",
                Status = false
            };
        }

        var findSession = await _context.Sessions
            .FirstOrDefaultAsync(x => x.Token == token);

        if (findSession == null)
        {
            return new Logout.LogoutResponse
            {
                Message = "Logout failed, cannot find session",
                Status = false
            };
        }

        _context.Sessions.Remove(findSession);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new Logout.LogoutResponse
            {
                Message = "Logout failed, db error",
                Status = false
            };
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Delete("Token");

        return new Logout.LogoutResponse
        {
            Message = "Logout success",
            Status = true
        };
    }
}