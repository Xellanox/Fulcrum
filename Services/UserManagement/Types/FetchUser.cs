namespace Fulcrum.Services.UserManagement.Types;

public class FetchUser
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
        public DateTime? Registration {get; set;}
        public DateTime? LastLogin {get; set;}
    }

    public class FetchUserRequest
    {
        public int Id {get; set;}
    }

    public class FetchUserResponse : BaseResponse 
    {
        public UserDetail Details {get; set;}
    }
}