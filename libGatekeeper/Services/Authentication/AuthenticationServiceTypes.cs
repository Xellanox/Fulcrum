using System.Data;

namespace libGatekeeper.Services.Authentication;

public class AuthenticationServiceTypes
{
    public class Login
    {
        public class LoginRequest
        {
            public string Username {get; set;}
            public string Password {get; set;}
        }

        public class LoginResponse : BaseResponse {}
    }

    public class Logout
    {
        public class LogoutResponse : BaseResponse {}
    }

    public class Register
    {
        public class RegisterRequest
        {
            public string Firstname {get; set;}
            public string Lastname {get; set;}

            public string Username {get; set;}
            public string Password {get; set;}
            public string Email {get; set;}
        }

        public class RegisterResponse : BaseResponse {}
    }
}