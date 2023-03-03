namespace libGatekeeper.Services.UserAdministration;

public class UserAdministrationServiceTypes
{
    public class UserListEntry
    {
        public int UserId {get; set;}
        
        public string Firstname {get; set;}
        public string Lastname {get; set;}

        public string Email {get; set;}
        public string Username {get; set;}
        
        public bool IsAdmin {get; set;}
        public bool IsEnabled {get; set;}

        public DateTime Registration {get; set;}
        public DateTime LastLogin {get; set;}
    }

    public class UserList
    {
        public class UserListResponse : BaseResponse
        {
            public List<UserListEntry> Users {get; set;}
        }
    }

    public class BanUser
    {
        public class BanUserRequest
        {
            public int UserId {get; set;}
        }

        public class BanUserResponse : BaseResponse {}
    }

    public class UnbanUser
    {
        public class UnbanUserRequest
        {
            public int UserId {get; set;}
        }

        public class UnbanUserResponse : BaseResponse {}
    }

    public class PromoteUser
    {
        public class PromoteUserRequest
        {
            public int UserId {get; set;}
        }

        public class PromoteUserResponse : BaseResponse {}
    }

    public class DemoteUser
    {
        public class DemoteUserRequest
        {
            public int UserId {get; set;}
        }

        public class DemoteUserResponse : BaseResponse {}
    }
}