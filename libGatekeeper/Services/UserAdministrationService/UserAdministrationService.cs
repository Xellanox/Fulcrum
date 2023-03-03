using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace libGatekeeper.Services.UserAdministration;

internal class UserAdministrationService : IUserAdministrationService
{
    private readonly GatekeeperContext _context;

    public UserAdministrationService(GatekeeperContext context)
    {
        _context = context;
    }

    public async Task<UserAdministrationServiceTypes.UserList.UserListResponse> GetUserList()
    {
        var Users = await _context.Users.AsNoTracking().ToListAsync();

        if (Users == null)
        {
            return new UserAdministrationServiceTypes.UserList.UserListResponse
            {
                Status = false,
                Message = "No users found"
            };
        }

        return new UserAdministrationServiceTypes.UserList.UserListResponse
        {
            Status = true,
            Message = "Users found",
            Users = Users.Select(x => new UserAdministrationServiceTypes.UserListEntry
            {
                UserId = x.UserId,
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                Email = x.Email,
                Username = x.Username,
                IsAdmin = x.IsAdmin,
                IsEnabled = x.IsEnabled,
                Registration = x.Registration,
                LastLogin = x.LastLogin
            }).ToList()
        };
    }

    public async Task<UserAdministrationServiceTypes.BanUser.BanUserResponse> BanUser(UserAdministrationServiceTypes.BanUser.BanUserRequest request)
    {
        var User = await _context.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId);

        if (User == null)
        {
            return new UserAdministrationServiceTypes.BanUser.BanUserResponse
            {
                Status = false,
                Message = "User not found"
            };
        }

        User.IsEnabled = false;

        await _context.SaveChangesAsync();

        return new UserAdministrationServiceTypes.BanUser.BanUserResponse
        {
            Status = true,
            Message = "User banned"
        };
    }

    public async Task<UserAdministrationServiceTypes.UnbanUser.UnbanUserResponse> UnbanUser(UserAdministrationServiceTypes.UnbanUser.UnbanUserRequest request)
    {
        var User = await _context.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId);

        if (User == null)
        {
            return new UserAdministrationServiceTypes.UnbanUser.UnbanUserResponse
            {
                Status = false,
                Message = "User not found"
            };
        }

        User.IsEnabled = true;

        await _context.SaveChangesAsync();

        return new UserAdministrationServiceTypes.UnbanUser.UnbanUserResponse
        {
            Status = true,
            Message = "User unbanned"
        };
    }

    public async Task<UserAdministrationServiceTypes.PromoteUser.PromoteUserResponse> PromoteUser(UserAdministrationServiceTypes.PromoteUser.PromoteUserRequest request)
    {
        var User = await _context.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId);

        if (User == null)
        {
            return new UserAdministrationServiceTypes.PromoteUser.PromoteUserResponse
            {
                Status = false,
                Message = "User not found"
            };
        }

        User.IsAdmin = true;

        await _context.SaveChangesAsync();

        return new UserAdministrationServiceTypes.PromoteUser.PromoteUserResponse
        {
            Status = true,
            Message = "User promoted"
        };
    }

    public async Task<UserAdministrationServiceTypes.DemoteUser.DemoteUserResponse> DemoteUser(UserAdministrationServiceTypes.DemoteUser.DemoteUserRequest request)
    {
        var User = await _context.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId);

        if (User == null)
        {
            return new UserAdministrationServiceTypes.DemoteUser.DemoteUserResponse
            {
                Status = false,
                Message = "User not found"
            };
        }

        User.IsAdmin = false;

        await _context.SaveChangesAsync();

        return new UserAdministrationServiceTypes.DemoteUser.DemoteUserResponse
        {
            Status = true,
            Message = "User demoted"
        };
    }
}