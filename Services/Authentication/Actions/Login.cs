using Fulcrum.Services.Authentication.Types;
using Microsoft.EntityFrameworkCore;
using Fulcrum.Models;

namespace Fulcrum.Services.Authentication;

internal partial class AuthenticationService : IAuthentication
{
    public async Task<Login.LoginResponse> Login(Login.LoginRequest request)
    {
        if (request.GetType().GetProperties().Any(x => x.GetValue(request).ToString() == ""))
        {
            return new Login.LoginResponse
            {
                Status = false,
                Message = "All Fields are required"
            };
        }

        var user = await _context.Users
            .Include(x => x.UserSessions)
            .FirstOrDefaultAsync(x => x.Username == request.Username && x.IsEnabled);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return new Login.LoginResponse
            {
                Status = false,
                Message = "Invalid Username or Password"
            };
        }

        var session = new Session
        {
            Token = BCrypt.Net.BCrypt.HashString($"{user.UserId}{DateTime.UtcNow.ToLongTimeString()}"),
            Issued = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(1),
            UserAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"],
            IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
        };

        user.UserSessions.Add(session);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new Login.LoginResponse
            {
                Status = false,
                Message = "An error occurred while logging in"
            };
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Append("Token", session.Token, new CookieOptions
        {
            Expires = session.Expires
        });

        return new Login.LoginResponse
        {
            Status = true,
            Message = "Login Successful"
        };
    }
}