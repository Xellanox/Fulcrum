using Fulcrum.Services.UserManagement.Types;

namespace Fulcrum.Services.UserManagement;

public interface IUserManagement
{
    Task<AddUser.AddUserResponse> AddUser(AddUser.AddUserRequest request);
    Task<DeleteUser.DeleteUserResponse> DeleteUser(DeleteUser.DeleteUserRequest request);
    Task<FetchUser.FetchUserResponse> FetchUser(FetchUser.FetchUserRequest request);
}