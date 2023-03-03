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

    [TypeFilter(typeof(Authenticate))]
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

    [TypeFilter(typeof(Authenticate))]
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

    [TypeFilter(typeof(Authenticate))]
    [HttpPost("api/v1/user")]
    public async Task<ActionResult<UserManagementServiceTypes.UpdateUser.UpdateUserResponse>> UpdateUser([FromBody] UserManagementServiceTypes.UpdateUser.UpdateUserRequest request)
    {
        var res = await _userManagementService.UpdateUser(request);

        if (res.Status)
        {
            return Ok(res);
        }
        else
        {
            return BadRequest(res);
        }
    }

    [TypeFilter(typeof(Authenticate))]
    [HttpDelete("api/v1/user")]
    public async Task<ActionResult<UserManagementServiceTypes.DeleteUser.DeleteUserResponse>> DeleteUser()
    {
        var res = await _userManagementService.DeleteUser();

        if (res.Status)
        {
            return Ok(res);
        }
        else
        {
            return BadRequest(res);
        }
    }

    [TypeFilter(typeof(Authenticate))]
    [HttpPost("api/v1/user/password")]
    public async Task<ActionResult<UserManagementServiceTypes.ChangePassword.ChangePasswordResponse>> ChangePassword([FromBody] UserManagementServiceTypes.ChangePassword.ChangePasswordRequest request)
    {
        var res = await _userManagementService.ChangePassword(request);

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