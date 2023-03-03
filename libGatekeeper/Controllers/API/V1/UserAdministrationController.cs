using Microsoft.AspNetCore.Mvc;
using libGatekeeper.Filters;
using libGatekeeper.Services.UserAdministration;

namespace libGatekeeper.Controllers.API;

public class UserAdministrationController : Controller
{
    private readonly IUserAdministrationService _userAdministrationService;

    public UserAdministrationController(IUserAdministrationService userAdministrationService)
    {
        _userAdministrationService = userAdministrationService;
    }

    [HttpGet]
    [TypeFilter(typeof(Administrative))]
    [Route("api/v1/user/list")]
    public async Task<UserAdministrationServiceTypes.UserList.UserListResponse> GetUserList()
    {
        return await _userAdministrationService.GetUserList();
    }

    [HttpPost]
    [TypeFilter(typeof(Administrative))]
    [Route("api/v1/user/ban")]
    public async Task<UserAdministrationServiceTypes.BanUser.BanUserResponse> BanUser([FromBody] UserAdministrationServiceTypes.BanUser.BanUserRequest request)
    {
        return await _userAdministrationService.BanUser(request);
    }

    [HttpPost]
    [TypeFilter(typeof(Administrative))]
    [Route("api/v1/user/unban")]
    public async Task<UserAdministrationServiceTypes.UnbanUser.UnbanUserResponse> UnbanUser([FromBody] UserAdministrationServiceTypes.UnbanUser.UnbanUserRequest request)
    {
        return await _userAdministrationService.UnbanUser(request);
    }

    [HttpPost]
    [TypeFilter(typeof(Administrative))]
    [Route("api/v1/user/promote")]
    public async Task<UserAdministrationServiceTypes.PromoteUser.PromoteUserResponse> PromoteUser([FromBody] UserAdministrationServiceTypes.PromoteUser.PromoteUserRequest request)
    {
        return await _userAdministrationService.PromoteUser(request);
    }

    [HttpPost]
    [TypeFilter(typeof(Administrative))]
    [Route("api/v1/user/demote")]
    public async Task<UserAdministrationServiceTypes.DemoteUser.DemoteUserResponse> DemoteUser([FromBody] UserAdministrationServiceTypes.DemoteUser.DemoteUserRequest request)
    {
        return await _userAdministrationService.DemoteUser(request);
    }
}

