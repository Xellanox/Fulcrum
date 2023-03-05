using Fulcrum.Models;
using Fulcrum.Services.UserManagement.Types;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Services.UserManagement;

internal partial class UserManagementService : IUserManagement
{
    public async Task<ListUser.ListUserResponse> ListUser(ListUser.ListUserRequest request)
    {
        var listing = new List<User>();

        if (request.PageSize != 0 && request.Start != 0)
        {
            listing = await _context.Users.AsNoTracking()
                .Skip(request.Start)
                .Take(request.PageSize)
                .ToListAsync();
        }
        else
        {
            listing = await _context.Users.AsNoTracking()
                .ToListAsync();
        }

        if (listing.Count == 0)
        {
            return new ListUser.ListUserResponse
            {
                Message = "Failed to get user listing",
                Status = false
            };
        }

        var responseList = new List<ListUser.UserListEntry>();

        foreach (var x in listing)
        {
            responseList.Add(new ListUser.UserListEntry
            {
                Firstname = x.Firstname,
                LastLogin = x.LastLogin,
                Lastname = x.Lastname,
                Registration = x.Registration,
                UserId = x.UserId,
                Username = x.Username
            });
        }

        return new ListUser.ListUserResponse
        {
            Message = "Got user listing",
            Status = true,
            UsersListing = responseList
        };
    }
}