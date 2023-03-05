using Fulcrum.Services.UserManagement.Types;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Services.CurrentUser;

internal partial class CurrentUserService : ICurrentUser
{
    public async Task<ChangePassword.ChangePasswordResponse> ChangePassword(ChangePassword.ChangePasswordRequest request)
    {
        if (request.GetType().GetProperties().Any(x => x.GetValue(request).ToString() == ""))
        {
            return new ChangePassword.ChangePasswordResponse
            {
                Message = "All fields are required",
                Status = false
            };
        }

        var currentUser = await FetchCurrentUser();

        var findUser = await _context.Users
            .FirstOrDefaultAsync(x => x.UserId == currentUser.Details.UserId);

        if (findUser == null)
        {
            return new ChangePassword.ChangePasswordResponse
            {
                Message = "Failed to determine current user",
                Status = false
            };
        }

        if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, findUser.Password))
        {
            return new ChangePassword.ChangePasswordResponse
            {
                Message = "Old password is incorrect",
                Status = false
            };
        }

        findUser.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new ChangePassword.ChangePasswordResponse
            {
                Message = "Erorr occurred while changing password",
                Status = false
            };
        }

        return new ChangePassword.ChangePasswordResponse
        {
            Message = "Successfully updated password",
            Status = true
        };



    }
}