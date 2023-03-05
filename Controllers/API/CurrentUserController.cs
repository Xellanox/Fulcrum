using Fulcrum.Filters;
using Fulcrum.Services.CurrentUser;
using Fulcrum.Services.CurrentUser.Types;
using Microsoft.AspNetCore.Mvc;

namespace Fulcrum.Controllers.API;

public class CurrentUserController : Controller
{
    private readonly ICurrentUser _currentUser;

    public CurrentUserController(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    [HttpGet]
    [TypeFilter(typeof(Authenticate))]
    [Route("/api/current")]
    public async Task<ActionResult<FetchCurrentUser.FetchCurrentUserResponse>> FetchCurrentUser()
        => await _currentUser.FetchCurrentUser();

    [HttpPatch]
    [TypeFilter(typeof(Authenticate))]
    [Route("/api/current/password")]
    public async Task<ActionResult<ChangePassword.ChangePasswordResponse>> ChangeCurentPassword([FromBody] ChangePassword.ChangePasswordRequest req)
        => await _currentUser.ChangePassword(req);

    [HttpPatch]
    [TypeFilter(typeof(Authenticate))]
    public async Task<ActionResult<UpdateCurrentUser.UpdateCurrentUserResponse>> UpdateCurrentUser([FromBody] UpdateCurrentUser.UpdateCurrentUserRequest req)
        => await _currentUser.UpdateCurrentUser(req);
}