namespace libGatekeeper.Services.UserManagement;

public interface IUserManagementService
{
    Task<UserManagementServiceTypes.FetchCurrentUser.FetchCurrentUserResponse> FetchCurrentUser();
    Task<UserManagementServiceTypes.FetchCurrentUserId.FetchCurrentUserIdResponse> FetchCurrentUserId();
}