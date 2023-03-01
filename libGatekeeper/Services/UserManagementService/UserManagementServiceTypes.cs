namespace libGatekeeper.Services.UserManagement;

public class UserManagementServiceTypes
{
    public class UserDetails
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Registered { get; set; }
        public DateTime LastLogin { get; set; }
    }
    
    public class FetchCurrentUser
    {
        public class FetchCurrentUserResponse : BaseResponse
        {
            public UserDetails User { get; set; }
        }
    }

    public class FetchCurrentUserId
    {
        public class FetchCurrentUserIdResponse : BaseResponse
        {
            public int UserId { get; set; }
        }
    }
}