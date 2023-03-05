using Fulcrum.Models;

namespace Fulcrum.Services.UserManagement.Types;

public class FetchCurrentUser
{
    public class FetchCurrentUserResponse : BaseResponse
    {
        public User Details {get; set;}
    }
}