using Fulcrum.Models;

namespace Fulcrum.Services.CurrentUser.Types;

public class FetchCurrentUser
{
    public class FetchCurrentUserResponse : BaseResponse
    {
        public User Details {get; set;}
    }
}