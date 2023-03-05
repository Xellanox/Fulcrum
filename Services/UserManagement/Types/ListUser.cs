namespace Fulcrum.Services.UserManagement.Types;

public class ListUser
{
    public class UserListEntry
    {
        public int UserId {get; set;}
        public string Username {get; set;}
        public string Firstname {get; set;}
        public string Lastname {get; set;}
        public DateTime? Registration {get; set;}
        public DateTime? LastLogin {get; set;}
    }

    public class ListUserRequest
    {
        public int Start {get; set;}
        public int PageSize {get; set;}
    }

    public class ListUserResponse : BaseResponse
    {
        public List<UserListEntry> UsersListing {get; set;}
    }
}