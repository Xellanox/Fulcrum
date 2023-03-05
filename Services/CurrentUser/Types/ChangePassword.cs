namespace Fulcrum.Services.CurrentUser.Types;

public class ChangePassword
{
    public class ChangePasswordRequest
    {
        public string OldPassword {get; set;}
        public string NewPassword {get; set;}
    }

    public class ChangePasswordResponse : BaseResponse {}
}