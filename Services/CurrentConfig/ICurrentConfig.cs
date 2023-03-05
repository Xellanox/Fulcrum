using Fulcrum.Services.CurrentConfig.Types;

namespace Fulcrum.Services.CurrentConfig;

public interface ICurrentConfig
{
    Task<FetchConfig.FetchConfigResponse> FetchConfig();
}