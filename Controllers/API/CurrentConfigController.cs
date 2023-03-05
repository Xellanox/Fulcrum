using Fulcrum.Filters;
using Fulcrum.Services.CurrentConfig;
using Fulcrum.Services.CurrentConfig.Types;
using Microsoft.AspNetCore.Mvc;

namespace Fulcrum.Controllers.API;

public class CurrentConfigController : Controller
{
    private readonly ICurrentConfig _currentConfig;

    public CurrentConfigController(ICurrentConfig currentConfig)
    {
        _currentConfig = currentConfig;
    }

    [HttpGet]
    [TypeFilter(typeof(Authenticate))]
    [Route("/api/config")]
    public async Task<ActionResult<FetchConfig.FetchConfigResponse>> FetchConfig()
        => await _currentConfig.FetchConfig();

    [HttpPatch]
    [TypeFilter(typeof(Administrative))]
    [Route("/api/config")]
    public async Task<ActionResult<WriteConfig.WriteConfigResponse>> WriteConfig([FromBody] WriteConfig.WriteConfigRequest req)
        => await _currentConfig.WriteConfig(req);
}