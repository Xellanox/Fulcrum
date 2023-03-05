using Fulcrum.Services.UserManagement;
using Fulcrum.Services.UserManagement.Types;
using Microsoft.AspNetCore.Mvc;

namespace Fulcrum.Controllers.API;

public class UserManagementController : Controller
{
    private readonly IUserManagement _userManagement;

    public UserManagementController(IUserManagement userManagement)
    {
        _userManagement = userManagement;
    }

    [HttpPut]
    [Route("/api/user")]
    public async Task<ActionResult<AddUser.AddUserResponse>> AddUser([FromBody] AddUser.AddUserRequest req) => await _userManagement.AddUser(req);

    [HttpDelete]
    [Route("/api/user")]
    public async Task<ActionResult<DeleteUser.DeleteUserResponse>> DeleteUser([FromBody] DeleteUser.DeleteUserRequest req) => await _userManagement.DeleteUser(req);

    [HttpGet]
    [Route("/api/user")]
    public async Task<ActionResult<FetchUser.FetchUserResponse>> FetchUser([FromBody] FetchUser.FetchUserRequest req) => await _userManagement.FetchUser(req);
}