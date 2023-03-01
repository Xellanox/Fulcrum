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
}