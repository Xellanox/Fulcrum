using BC = BCrypt.Net.BCrypt;
using libGatekeeper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace libGatekeeper.Services.Authentication;

internal class AuthenticationService : IAuthenticationService
{
    private readonly GatekeeperContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public AuthenticationService(GatekeeperContext context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
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

        if (!findUser.IsEnabled)
        {
            return new AuthenticationServiceTypes.Login.LoginResponse
            {
                Status = false,
                Message = "User is disabled"
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
        if (_configuration.GetSection("GatekeeperConfig").GetSection("AllowSelfServiceRegistration").Value == "false")
        {
            return new AuthenticationServiceTypes.Register.RegisterResponse
            {
                Status = false,
                Message = "Self service registration is disabled"
            };
        }

        if (request.GetType().GetProperties().Any(x => x.GetValue(request) == null))
        {
            return new AuthenticationServiceTypes.Register.RegisterResponse
            {
                Status = false,
                Message = "All fields are required"
            };
        }

        if (request.Password.Length < Convert.ToInt32(_configuration.GetSection("GatekeeperConfig").GetSection("PasswordMinLength").Value))
        {
            return new AuthenticationServiceTypes.Register.RegisterResponse
            {
                Status = false,
                Message = $"Password must be at least {_configuration.GetSection("GatekeeperConfig").GetSection("PasswordMinLength").Value} characters"
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