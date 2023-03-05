using Fulcrum.Models;
using Microsoft.EntityFrameworkCore;
using Fulcrum.Services.UserManagement.Types;

namespace Fulcrum.Services.UserManagement;

internal partial class UserManagementService : IUserManagement
{
    public async Task<AddUser.AddUserResponse> AddUser(AddUser.AddUserRequest request)
    {
        if (request.GetType().GetProperties().Any(x => x.GetValue(request).ToString() == ""))
        {
            return new AddUser.AddUserResponse
            {
                Message = "All fields are required",
                Status = false
            };
        }

        if (await _context.Users.AsNoTracking().AnyAsync(x => x.Username == request.Username || x.Email == request.Email))
        {
            return new AddUser.AddUserResponse
            {
                Message = "Username or email already in use",
                Status = false
            };
        }

        await _context.Users.AddAsync(new User
        {
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Email = request.Email,
            IsAdmin = false,
            IsEnabled = true,
            Registration = DateTime.UtcNow,
            Username = request.Username
        });

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new AddUser.AddUserResponse
            {
                Message = "An error occured while registering",
                Status = false
            };
        }

        return new AddUser.AddUserResponse
        {
            Message = "Registration success",
            Status = true
        };
    }
}