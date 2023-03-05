namespace Fulcrum.Services.CurrentUser.Types;

public class UpdateCurrentUser
{
    public class UpdateCurrentUserRequest
    {
        public string Firstname {get; set;}
        public string Lastname {get; set;}
        public string Username {get; set;}
        public string Email {get; set;}
    }

    public class UpdateCurrentUserResponse : BaseResponse {}
}