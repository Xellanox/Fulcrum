using BC = BCrypt.Net.BCrypt;
using libGatekeeper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace libGatekeeper.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly GatekeeperContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(GatekeeperContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthenticationServiceTypes.Login.LoginResponse> Login(AuthenticationServiceTypes.Login.LoginRequest request)
    {
        var findUser = await _context.Users
        .Include(x => x.UserSessions)
        .FirstOrDefaultAsync(x => x.Username == request.Username);

        if (findUser == null)
        {
            return new AuthenticationServiceTypes.Login.LoginResponse
            {
                Status = false,
                Message = "User not found"
            };
        }

        if (!BC.Verify(request.Password, findUser.Password))
        {
            return new AuthenticationServiceTypes.Login.LoginResponse
            {
                Status = false,
                Message = "Invalid password"
            };
        }

        var token = Guid.NewGuid().ToString().Replace("-", "");

        findUser.UserSessions.Add(new Session{
            Token = token,
            Expires = DateTime.UtcNow.AddDays(1),
            Issued = DateTime.UtcNow,
            IP = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
            UserAgent = _httpContextAccessor.HttpContext.Request.Headers.UserAgent.ToString(),
        });

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new AuthenticationServiceTypes.Login.LoginResponse
            {
                Status = false,
                Message = "An error occurred while logging in"
            };
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Append("Token", token);

        return new AuthenticationServiceTypes.Login.LoginResponse
        {
            Status = true,
            Message = "Logged in successfully"
        };
    }

    public async Task<AuthenticationServiceTypes.Logout.LogoutResponse> Logout()
    {
        var currentToken = _httpContextAccessor.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "Token").Value;

        var findSession = await _context.Sessions.FirstOrDefaultAsync(x => x.Token == currentToken && x.Expires > DateTime.UtcNow);

        if (findSession == null)
        {
            return new AuthenticationServiceTypes.Logout.LogoutResponse
            {
                Status = false,
                Message = "Session not found, Logout failed"
            };
        }

        _context.Sessions.Remove(findSession);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new AuthenticationServiceTypes.Logout.LogoutResponse
            {
                Status = false,
                Message = "An error occurred while logging out"
            };
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Delete("Token");

        return new AuthenticationServiceTypes.Logout.LogoutResponse
        {
            Status = true,
            Message = "Logged out successfully"
        };
    }

    public async Task<AuthenticationServiceTypes.Register.RegisterResponse> Register(AuthenticationServiceTypes.Register.RegisterRequest request)
    {
        if (request.GetType().GetProperties().Any(x => x.GetValue(request) == null))
        {
            return new AuthenticationServiceTypes.Register.RegisterResponse
            {
                Status = false,
                Message = "All fields are required"
            };
        }

        if (await _context.Users.AsNoTracking().AnyAsync(x => x.Username == request.Username || x.Email == request.Email))
        {
            return new AuthenticationServiceTypes.Register.RegisterResponse
            {
                Status = false,
                Message = "Username or email already exists"
            };
        }
        
        await _context.Users.AddAsync(new User(){
            Email = request.Email,
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            Password = BC.HashPassword(request.Password),
            Username = request.Username,
        });
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new AuthenticationServiceTypes.Register.RegisterResponse
            {
                Status = false,
                Message = "An error occurred while registering"
            };
        }

        return new AuthenticationServiceTypes.Register.RegisterResponse
        {
            Status = true,
            Message = "Registered successfully"
        };
    }
}