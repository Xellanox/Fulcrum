namespace Fulcrum.Services.UserManagement.Types;

public class DeleteUser
{
    public class DeleteUserRequest
    {
        public int UserId {get; set;}
    }

    public class DeleteUserResponse : BaseResponse {}
}