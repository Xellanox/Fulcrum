using Fulcrum.Services.UserManagement.Types;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Services.UserManagement;

internal partial class UserManagementService : IUserManagement
{
    public async Task<DeleteUser.DeleteUserResponse> DeleteUser(DeleteUser.DeleteUserRequest req)
    {
        var currentUser = await _currentUser.FetchCurrentUser();

        var findUser = await _context.Users
            .FirstOrDefaultAsync(x => x.UserId == req.UserId);

        if (findUser == null)
        {
            return new DeleteUser.DeleteUserResponse
            {
                Message = "Specified user was not found",
                Status = false
            };
        }

        _context.RemoveRange(await _context.Sessions.Where(x => x.UserId == req.UserId).ToListAsync());
        _context.Remove(findUser);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new DeleteUser.DeleteUserResponse
            {
                Message = "Error occured while deleting user",
                Status = false
            };
        }

        if (req.UserId == currentUser.Details.UserId)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("Token");
        }

        return new DeleteUser.DeleteUserResponse
        {
            Message = "Successfully deleted user",
            Status = true
        };
    }
}