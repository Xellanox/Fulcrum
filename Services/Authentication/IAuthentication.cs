using Fulcrum.Services.Authentication.Types;
 
namespace Fulcrum.Services.Authentication;

public interface IAuthentication
{
    Task<Login.LoginResponse> Login(Login.LoginRequest request);
    Task<Logout.LogoutResponse> Logout();
}