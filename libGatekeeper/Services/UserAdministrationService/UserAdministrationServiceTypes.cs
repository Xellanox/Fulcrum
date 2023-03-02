using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.Identity.Client;

namespace libGatekeeper.Services.UserAdministration;

public class UserAdministrationServiceTypes
{
    public class UserDetails
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
            public List<UserDetails> Users {get; set;}
        }
    }
}