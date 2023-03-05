using Fulcrum.Services.CurrentUser.Types;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Services.CurrentUser;

internal partial class CurrentUserService : ICurrentUser
{
    public async Task<FetchCurrentUser.FetchCurrentUserResponse> FetchCurrentUser()
    {
        var currentToken = _httpContextAccessor.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "Token").Value;

        if (currentToken == "" || currentToken == null)
        {
            return new FetchCurrentUser.FetchCurrentUserResponse
            {
                Message = "No token present",
                Status = false
            };
        }

        var findSession = await _context.Sessions.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Token == currentToken);

        if (findSession == null)
        {
            return new FetchCurrentUser.FetchCurrentUserResponse
            {
                Message = "No valid session present",
                Status = false
            };
        }

        return new FetchCurrentUser.FetchCurrentUserResponse
        {
            Message = "Got current user",
            Status = true,
            Details = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == findSession.UserId)
        };
    }
}