using Fulcrum.Models;

namespace Fulcrum.Services.CurrentConfig.Types;

public class WriteConfig
{
    public class WriteConfigRequest
    {
        public SysConfig NewConfig {get; set;}
    }

    public class WriteConfigResponse : BaseResponse {}
}