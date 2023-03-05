using Fulcrum.Services.CurrentUser.Types;

namespace Fulcrum.Services.CurrentUser;

public interface ICurrentUser
{
    Task<FetchCurrentUser.FetchCurrentUserResponse> FetchCurrentUser();
    Task<ChangePassword.ChangePasswordResponse> ChangePassword(ChangePassword.ChangePasswordRequest request);
}