namespace Fulcrum.Services.UserManagement.Types;

public class UpdateUser
{
    public class UserDetail
    {
        public int UserId {get; set;}
        public string Firstname {get; set;}
        public string Lastname {get; set;}
        public string Username {get; set;}
        public string Email {get; set;}
        public bool IsAdmin {get; set;}
        public bool IsEnabled {get; set;}
    }

    public class UpdateUserRequest
    {
        public UserDetail UpdatedDetails {get; set;}
    }

    public class UpdateUserResponse : BaseResponse {}
}