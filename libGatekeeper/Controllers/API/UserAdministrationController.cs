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
    [Route("api/user/list")]
    public async Task<UserAdministrationServiceTypes.UserList.UserListResponse> GetUserList()
    {
        return await _userAdministrationService.GetUserList();
    }
}

