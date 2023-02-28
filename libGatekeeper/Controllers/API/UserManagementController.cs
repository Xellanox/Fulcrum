using Microsoft.AspNetCore.Mvc;
using libGatekeeper.Services.UserManagement;
using libGatekeeper.Filters;

namespace libGatekeeper.Controllers.API;

public class UserManagementController : Controller
{
    private readonly IUserManagementService _userManagementService;

    public UserManagementController(IUserManagementService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    [Authenticate]
    [HttpGet("api/v1/user")]
    public async Task<ActionResult<UserManagementServiceTypes.FetchCurrentUser.FetchCurrentUserResponse>> FetchCurrentUser()
    {
        var res = await _userManagementService.FetchCurrentUser();

        if (res.Status)
        {
            return Ok(res);
        }
        else
        {
            return BadRequest(res);
        }
    }

    [Authenticate]
    [HttpGet("api/v1/user/id")]
    public async Task<ActionResult<UserManagementServiceTypes.FetchCurrentUserId.FetchCurrentUserIdResponse>> FetchCurrentUserId()
    {
        var res = await _userManagementService.FetchCurrentUserId();

        if (res.Status)
        {
            return Ok(res);
        }
        else
        {
            return BadRequest(res);
        }
    }
}