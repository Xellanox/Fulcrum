namespace libGatekeeper.Services.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationServiceTypes.Login.LoginResponse> Login(AuthenticationServiceTypes.Login.LoginRequest request);
    Task<AuthenticationServiceTypes.Logout.LogoutResponse> Logout();
    Task<AuthenticationServiceTypes.Register.RegisterResponse> Register(AuthenticationServiceTypes.Register.RegisterRequest request);
}