using Fulcrum.Services.CurrentUser.Types;

namespace Fulcrum.Services.CurrentUser;

public interface ICurrentUser
{
    Task<FetchCurrentUser.FetchCurrentUserResponse> FetchCurrentUser();
    Task<ChangePassword.ChangePasswordResponse> ChangePassword(ChangePassword.ChangePasswordRequest request);
    Task<UpdateCurrentUser.UpdateCurrentUserResponse> UpdateCurrentUser(UpdateCurrentUser.UpdateCurrentUserRequest request);
    Task<DeleteCurrentUser.DeleteCurrentUserResponse> DeleteCurrentUser();
}