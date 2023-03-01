using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace libGatekeeper.Services.UserManagement;

public class UserManagementService : IUserManagementService
{
    private readonly GatekeeperContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserManagementService(GatekeeperContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
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
}