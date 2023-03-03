namespace libGatekeeper.Services.UserAdministration;

public interface IUserAdministrationService
{
    Task<UserAdministrationServiceTypes.UserList.UserListResponse> GetUserList();
    Task<UserAdministrationServiceTypes.BanUser.BanUserResponse> BanUser(UserAdministrationServiceTypes.BanUser.BanUserRequest request);
    Task<UserAdministrationServiceTypes.UnbanUser.UnbanUserResponse> UnbanUser(UserAdministrationServiceTypes.UnbanUser.UnbanUserRequest request);
    Task<UserAdministrationServiceTypes.PromoteUser.PromoteUserResponse> PromoteUser(UserAdministrationServiceTypes.PromoteUser.PromoteUserRequest request);
    Task<UserAdministrationServiceTypes.DemoteUser.DemoteUserResponse> DemoteUser(UserAdministrationServiceTypes.DemoteUser.DemoteUserRequest request);
}