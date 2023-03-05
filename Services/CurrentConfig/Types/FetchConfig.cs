using Fulcrum.Models;

namespace Fulcrum.Services.CurrentConfig.Types;

public class FetchConfig
{
    public class FetchConfigResponse : BaseResponse
    {
        public SysConfig CurrentConfig {get; set;}
    }
}