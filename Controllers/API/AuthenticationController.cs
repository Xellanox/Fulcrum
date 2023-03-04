using Microsoft.AspNetCore.Mvc;
using Fulcrum.Services.Authentication;
using Fulcrum.Services.Authentication.Types;

namespace Fulcrum.Controllers.API;

public class AuthenticationController : Controller
{
    private readonly IAuthentication _authentication;

    public AuthenticationController(IAuthentication authentication)
    {
        _authentication = authentication;
    }

    [HttpPost]
    [Route("/api/login")]
    public async Task<ActionResult<Login.LoginResponse>> Login([FromBody] Login.LoginRequest req)
    {
        var res = await _authentication.Login(req);

        return res.Status
        ? Ok(res)
        : BadRequest(res);
    }

    [HttpPost]
    [Route("/api/logout")]
    public async Task<ActionResult<Logout.LogoutResponse>> Logout()
    {
        var res = await _authentication.Logout();

        return res.Status
        ? Ok(res)
        : BadRequest(res);
    }
}