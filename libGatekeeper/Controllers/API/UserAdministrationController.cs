using Microsoft.AspNetCore.Mvc;
using libGatekeeper.Filters;

namespace libGatekeeper.Controllers.API;

public class UserAdministrationController : Controller
{
    private readonly Services.UserAdministration.IUserAdministrationService _userAdministrationService;

    public UserAdministrationController(Services.UserAdministration.IUserAdministrationService userAdministrationService)
    {
        _userAdministrationService = userAdministrationService;
    }

    [HttpGet]
    [Administrative]
    public async Task<Services.UserAdministration.UserAdministrationServiceTypes.UserList.UserListResponse> GetUserList()
    {
        return await _userAdministrationService.GetUserList();
    }
}

