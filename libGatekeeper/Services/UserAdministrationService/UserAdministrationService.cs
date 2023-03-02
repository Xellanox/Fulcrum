using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace libGatekeeper.Services.UserAdministration;

public class UserAdministrationService : IUserAdministrationService
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
            Users = Users.Select(x => new UserAdministrationServiceTypes.UserDetails
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
}