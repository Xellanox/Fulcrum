using Fulcrum.Services.UserManagement;
using Fulcrum.Services.UserManagement.Types;
using Microsoft.AspNetCore.Mvc;
using Fulcrum.Filters;

namespace Fulcrum.Controllers.API;

public class UserManagementController : Controller
{
    private readonly IUserManagement _userManagement;

    public UserManagementController(IUserManagement userManagement)
    {
        _userManagement = userManagement;
    }

    [HttpPut]
    [TypeFilter(typeof(Administrative))]
    [Route("/api/user")]
    public async Task<ActionResult<AddUser.AddUserResponse>> AddUser([FromBody] AddUser.AddUserRequest req) => await _userManagement.AddUser(req);

    [HttpDelete]
    [TypeFilter(typeof(Administrative))]
    [Route("/api/user")]
    public async Task<ActionResult<DeleteUser.DeleteUserResponse>> DeleteUser([FromBody] DeleteUser.DeleteUserRequest req) => await _userManagement.DeleteUser(req);

    [HttpGet]
    [TypeFilter(typeof(Administrative))]
    [Route("/api/user")]
    public async Task<ActionResult<FetchUser.FetchUserResponse>> FetchUser([FromBody] FetchUser.FetchUserRequest req) => await _userManagement.FetchUser(req);

    [HttpPatch]
    [TypeFilter(typeof(Administrative))]
    [Route("/api/user")]
    public async Task<ActionResult<UpdateUser.UpdateUserResponse>> UpdateUser([FromBody] UpdateUser.UpdateUserRequest req) => await _userManagement.UpdateUser(req);

    [HttpGet]
    [TypeFilter(typeof(Administrative))]
    [Route("/api/user/list")]
    public async Task<ActionResult<ListUser.ListUserResponse>> ListUser([FromBody] ListUser.ListUserRequest req) => await _userManagement.ListUser(req);
}