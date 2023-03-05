namespace Fulcrum.Services.UserManagement.Types;

public class AddUser
{
    public class AddUserRequest
    {
        public string Firstname {get; set;}
        public string Lastname {get; set;}
        public string Username {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
    }

    public class AddUserResponse : BaseResponse { }
}