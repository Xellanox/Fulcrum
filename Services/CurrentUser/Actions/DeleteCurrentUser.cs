using Fulcrum.Services.CurrentUser.Types;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Services.CurrentUser;

internal partial class CurrentUserService : ICurrentUser
{
    public async Task<DeleteCurrentUser.DeleteCurrentUserResponse> DeleteCurrentUser()
    {
        var currentUser = await FetchCurrentUser();

        if (currentUser == null)
        {
            return new DeleteCurrentUser.DeleteCurrentUserResponse
            {
                Message = "Failed to determine current user",
                Status = false
            };
        }

        _context.Sessions.RemoveRange(await _context.Sessions.Where(x => x.UserId == currentUser.Details.UserId).ToListAsync());
        _context.Users.Remove(await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentUser.Details.UserId));


        try
        {
            await _context.SaveChangesAsync();
        }
        catch(Exception)
        {
            return new DeleteCurrentUser.DeleteCurrentUserResponse
            {
                Message = "Erorr occurred while deleting current user",
                Status = false
            };
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Delete("Token");

        return new DeleteCurrentUser.DeleteCurrentUserResponse
        {
            Message = "Successfully deleted current user, goodbye",
            Status = true
        };
    }
}