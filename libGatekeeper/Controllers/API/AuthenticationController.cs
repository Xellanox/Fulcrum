using Microsoft.AspNetCore.Mvc;
using libGatekeeper.Services.Authentication;

namespace libGatekeeper.Controllers.API;

public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("api/v1/auth/login")]
    public async Task<ActionResult<AuthenticationServiceTypes.Login.LoginResponse>> Login([FromBody] AuthenticationServiceTypes.Login.LoginRequest request)
    {
        var response = await _authenticationService.Login(request);

        if (response.Status)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost]
    [Route("api/v1/auth/logout")]
    public async Task<ActionResult<AuthenticationServiceTypes.Logout.LogoutResponse>> Logout()
    {
        var response = await _authenticationService.Logout();

        if (response.Status)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPost]
    [Route("api/v1/auth/register")]
    public async Task<ActionResult<AuthenticationServiceTypes.Register.RegisterResponse>> Register([FromBody] AuthenticationServiceTypes.Register.RegisterRequest request)
    {
        var response = await _authenticationService.Register(request);

        if (response.Status)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}