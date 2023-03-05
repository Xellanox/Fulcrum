using Fulcrum.Services.UserManagement.Types;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Services.UserManagement;

internal partial class UserManagementService : IUserManagement
{
    public async Task<FetchUser.FetchUserResponse> FetchUser(FetchUser.FetchUserRequest request)
    {
        var findUser = await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == request.Id);

        if (findUser == null)
        {
            return new FetchUser.FetchUserResponse
            {
                Message = "User not found",
                Status = false
            };
        }

        return new FetchUser.FetchUserResponse
        {
            Message = "Successfully got user info",
            Status = true,
            Details = new FetchUser.UserDetail
            {
                Firstname = findUser.Firstname,
                Lastname = findUser.Lastname,
                Email = findUser.Email,
                IsAdmin = findUser.IsAdmin,
                IsEnabled = findUser.IsEnabled,
                LastLogin = findUser.LastLogin,
                Registration = findUser.Registration,
                UserId = findUser.UserId,
                Username = findUser.Username
            }
        };
    }
}