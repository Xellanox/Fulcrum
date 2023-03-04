namespace Fulcrum.Services.Authentication.Types;

public class Login
{
    public class LoginRequest
    {
        public string Username {get; set;}
        public string Password {get; set;}
    }

    public class LoginResponse : BaseResponse { }
}