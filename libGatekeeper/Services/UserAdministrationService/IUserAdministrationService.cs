namespace libGatekeeper.Services.UserAdministration;

public interface IUserAdministrationService
{
    Task<UserAdministrationServiceTypes.UserList.UserListResponse> GetUserList();
}