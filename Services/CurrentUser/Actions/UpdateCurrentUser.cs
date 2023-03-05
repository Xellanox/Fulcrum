using Fulcrum.Services.CurrentUser.Types;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Services.CurrentUser;

internal partial class CurrentUserService : ICurrentUser
{
    public async Task<UpdateCurrentUser.UpdateCurrentUserResponse> UpdateCurrentUser(UpdateCurrentUser.UpdateCurrentUserRequest request)
    {
        if (request.GetType().GetProperties().Any(x => x.GetValue(request).ToString() == ""))
        {
            return new UpdateCurrentUser.UpdateCurrentUserResponse
            {
                Message = "All fields are required",
                Status = false
            };
        }

        var currentUser = await FetchCurrentUser();

        if (currentUser == null)
        {
            return new UpdateCurrentUser.UpdateCurrentUserResponse
            {
                Message = "Could not determine current user",
                Status = false
            };
        }

        var target = await _context.Users
            .FirstOrDefaultAsync(x => x.UserId == currentUser.Details.UserId);

        if (target == null)
        {
            return new UpdateCurrentUser.UpdateCurrentUserResponse
            {
                Message = "Could not open current user for editing",
                Status = false
            };
        }

        target.Firstname = request.Firstname;
        target.Lastname = request.Lastname;

        if (await _context.Users.AsNoTracking()
        .Where(x => x.UserId != target.UserId)
        .AnyAsync(x => x.Username == request.Username))
        {
            return new UpdateCurrentUser.UpdateCurrentUserResponse
            {
                Message = "Propsed username change would cause a conflict",
                Status = false
            };
        }

        target.Username = request.Username;

        if (await _context.Users.AsNoTracking()
            .Where(x => x.UserId != target.UserId)
            .AnyAsync(x => x.Email == request.Email))
        {
            return new UpdateCurrentUser.UpdateCurrentUserResponse
            {
                Message = "Propsed email change would cause a conflict",
                Status = false
            };
        }

        target.Email = request.Email;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new UpdateCurrentUser.UpdateCurrentUserResponse
            {
                Message = "Error occured while updating current user",
                Status = false
            };
        }

        return new UpdateCurrentUser.UpdateCurrentUserResponse
        {
            Message = "Successfully updated current user",
            Status = true
        };
    }
}