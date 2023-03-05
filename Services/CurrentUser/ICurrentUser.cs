using Fulcrum.Services.UserManagement.Types;

namespace Fulcrum.Services.CurrentUser;

public interface ICurrentUser
{
    Task<FetchCurrentUser.FetchCurrentUserResponse> FetchCurrentUser();
    Task<ChangePassword.ChangePasswordResponse> ChangePassword(ChangePassword.ChangePasswordRequest request);
}