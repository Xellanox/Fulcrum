using Fulcrum.Services.UserManagement.Types;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Services.UserManagement;

internal partial class UserManagementService : IUserManagement
{
    public async Task<UpdateUser.UpdateUserResponse> UpdateUser(UpdateUser.UpdateUserRequest request)
    {
        var target  = await _context.Users
            .FirstOrDefaultAsync(x => x.UserId == request.UpdatedDetails.UserId);

        if (target == null)
        {
            return new UpdateUser.UpdateUserResponse
            {
                Message = "Target user not found",
                Status = false
            };
        }

        target.Firstname = target.Firstname != request.UpdatedDetails.Firstname && request.UpdatedDetails.Firstname != ""
            ? request.UpdatedDetails.Firstname
            : target.Firstname;

        target.Lastname = target.Lastname != request.UpdatedDetails.Lastname && request.UpdatedDetails.Lastname != ""
            ? request.UpdatedDetails.Lastname
            : target.Lastname;

        if (await _context.Users.AsNoTracking()
            .Where(x => x.UserId != target.UserId)
            .AnyAsync(x => x.Username == request.UpdatedDetails.Username))
        {
            return new UpdateUser.UpdateUserResponse
            {
                Message = "Proposed username change would cause a conflict",
                Status = false
            };
        }

        target.Username = target.Username != request.UpdatedDetails.Username && request.UpdatedDetails.Username != ""
            ? request.UpdatedDetails.Username
            : target.Username;

        if (await _context.Users.AsNoTracking()
            .Where(x => x.UserId != target.UserId)
            .AnyAsync(x => x.Email == request.UpdatedDetails.Email))
        {
            return new UpdateUser.UpdateUserResponse
            {
                Message = "Proposed email change would cause a conflict",
                Status = false
            };
        }

        target.Email = target.Email != request.UpdatedDetails.Email && request.UpdatedDetails.Email != ""
            ? request.UpdatedDetails.Email
            : target.Email;

        target.IsAdmin = request.UpdatedDetails.IsAdmin;
        target.IsEnabled = request.UpdatedDetails.IsEnabled;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new UpdateUser.UpdateUserResponse
            {
                Message = "Error occurred while updated user details",
                Status = false
            };
        }

        return new UpdateUser.UpdateUserResponse
        {
            Message = "Successfully updated target user",
            Status = true
        };

    }
}