using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace libGatekeeper.Services.UserManagement;

public class UserManagementService : IUserManagementService
{
    private readonly GatekeeperContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public UserManagementService(GatekeeperContext context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public async Task<UserManagementServiceTypes.FetchCurrentUser.FetchCurrentUserResponse>FetchCurrentUser()
    {
        var currentToken = _httpContextAccessor.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "Token").Value;

        var session = await _context.Sessions.AsNoTracking().FirstOrDefaultAsync(x => x.Token == currentToken && x.Expires > DateTime.UtcNow);

        if (session == null)
        {
            return new UserManagementServiceTypes.FetchCurrentUser.FetchCurrentUserResponse
            {
                Status = false,
                Message = "Invalid session"
            };
        }

        var findUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == session.UserId);

        if (findUser == null)
        {
            return new UserManagementServiceTypes.FetchCurrentUser.FetchCurrentUserResponse
            {
                Status = false,
                Message = "Invalid user"
            };
        }

        return new UserManagementServiceTypes.FetchCurrentUser.FetchCurrentUserResponse
        {
            Status = true,
            Message = "Success",
            User = new UserManagementServiceTypes.UserDetails
            {
                UserId = findUser.UserId,
                Username = findUser.Username,
                Email = findUser.Email,
                FirstName = findUser.Firstname,
                LastName = findUser.Lastname,
                Registered = findUser.Registration,
                LastLogin = findUser.LastLogin
            }
        };
    }

    public async Task<UserManagementServiceTypes.FetchCurrentUserId.FetchCurrentUserIdResponse> FetchCurrentUserId()
    {
        var currentToken = _httpContextAccessor.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "Token").Value;

        var currentSession = await _context.Sessions.AsNoTracking().FirstOrDefaultAsync(x => x.Token == currentToken && x.Expires > DateTime.UtcNow);

        if (currentSession == null)
        {
            return new UserManagementServiceTypes.FetchCurrentUserId.FetchCurrentUserIdResponse
            {
                Status = false,
                Message = "Invalid session"
            };
        }

        return new UserManagementServiceTypes.FetchCurrentUserId.FetchCurrentUserIdResponse
        {
            Status = true,
            Message = "Success",
            UserId = currentSession.UserId
        }; 
    }

    public async Task<UserManagementServiceTypes.UpdateUser.UpdateUserResponse> UpdateUser(UserManagementServiceTypes.UpdateUser.UpdateUserRequest request)
    {
        var currentToken = _httpContextAccessor.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "Token").Value;

        var currentSession = await _context.Sessions.AsNoTracking().FirstOrDefaultAsync(x => x.Token == currentToken && x.Expires > DateTime.UtcNow);

        if (currentSession == null)
        {
            return new UserManagementServiceTypes.UpdateUser.UpdateUserResponse
            {
                Status = false,
                Message = "Invalid session"
            };
        }

        if (await _context.Users.AsNoTracking().Where(x => x.UserId == currentSession.UserId).AnyAsync(x => x.Username == request.Username || x.Email == request.Email))
        {
            return new UserManagementServiceTypes.UpdateUser.UpdateUserResponse
            {
                Status = false,
                Message = "Username or email already in use by another user"
            };
        }

        var findUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentSession.UserId);

        if (findUser == null)
        {
            return new UserManagementServiceTypes.UpdateUser.UpdateUserResponse
            {
                Status = false,
                Message = "Invalid user"
            };
        }

        findUser.Username = request.Username != "" ? request.Username : findUser.Username;
        findUser.Email = request.Email != "" ? request.Email : findUser.Email;
        findUser.Firstname = request.FirstName != "" ? request.FirstName : findUser.Firstname;
        findUser.Lastname = request.LastName != "" ? request.LastName : findUser.Lastname;

        await _context.SaveChangesAsync();

        return new UserManagementServiceTypes.UpdateUser.UpdateUserResponse
        {
            Status = true,
            Message = "Successfully updated user"
        };
    }

    public async Task<UserManagementServiceTypes.DeleteUser.DeleteUserResponse> DeleteUser()
    {
        var currentToken = _httpContextAccessor.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "Token").Value;

        var currentSession = await _context.Sessions.AsNoTracking().FirstOrDefaultAsync(x => x.Token == currentToken && x.Expires > DateTime.UtcNow);

        if (currentSession == null)
        {
            return new UserManagementServiceTypes.DeleteUser.DeleteUserResponse
            {
                Status = false,
                Message = "Invalid session"
            };
        }

        var findUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentSession.UserId);

        if (findUser == null)
        {
            return new UserManagementServiceTypes.DeleteUser.DeleteUserResponse
            {
                Status = false,
                Message = "Invalid user"
            };
        }

        _context.Users.Remove(findUser);
        _context.Sessions.RemoveRange(await _context.Sessions.Where(x => x.UserId == currentSession.UserId).ToListAsync());

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new UserManagementServiceTypes.DeleteUser.DeleteUserResponse
            {
                Status = false,
                Message = "Failed to delete user"
            };
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Delete("Token");

        return new UserManagementServiceTypes.DeleteUser.DeleteUserResponse
        {
            Status = true,
            Message = "Successfully deleted user"
        };
    }

    public async Task<UserManagementServiceTypes.ChangePassword.ChangePasswordResponse> ChangePassword(UserManagementServiceTypes.ChangePassword.ChangePasswordRequest request)
    {
        var currentToken = _httpContextAccessor.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "Token").Value;

        var currentSession = await _context.Sessions.AsNoTracking().FirstOrDefaultAsync(x => x.Token == currentToken && x.Expires > DateTime.UtcNow);

        if (currentSession == null)
        {
            return new UserManagementServiceTypes.ChangePassword.ChangePasswordResponse
            {
                Status = false,
                Message = "Invalid session"
            };
        }

        var findUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentSession.UserId);

        if (findUser == null)
        {
            return new UserManagementServiceTypes.ChangePassword.ChangePasswordResponse
            {
                Status = false,
                Message = "Invalid user"
            };
        }

        if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, findUser.Password))
        {
            return new UserManagementServiceTypes.ChangePassword.ChangePasswordResponse
            {
                Status = false,
                Message = "Current password is incorrect"
            };
        }

        if (request.NewPassword.Length < _configuration.GetSection("GatekeeperConfig").GetSection("PasswordMinLength").Get<int>())
        {
            return new UserManagementServiceTypes.ChangePassword.ChangePasswordResponse
            {
                Status = false,
                Message = $"New password must be at least {_configuration.GetSection("GatekeeperConfig").GetSection("PasswordMinLength").Get<int>()} characters"
            };
        }

        findUser.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

        await _context.SaveChangesAsync();

        return new UserManagementServiceTypes.ChangePassword.ChangePasswordResponse
        {
            Status = true,
            Message = "Successfully changed password"
        };
    }
}