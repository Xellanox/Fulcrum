using Fulcrum.Services.UserManagement.Types;

namespace Fulcrum.Services.UserManagement;

public interface IUserManagement
{
    Task<AddUser.AddUserResponse> AddUser(AddUser.AddUserRequest request);
    Task<DeleteUser.DeleteUserResponse> DeleteUser(DeleteUser.DeleteUserRequest request);
    Task<FetchUser.FetchUserResponse> FetchUser(FetchUser.FetchUserRequest request);
    Task<UpdateUser.UpdateUserResponse> UpdateUser(UpdateUser.UpdateUserRequest request);
    Task<ListUser.ListUserResponse> ListUser(ListUser.ListUserRequest request);
}