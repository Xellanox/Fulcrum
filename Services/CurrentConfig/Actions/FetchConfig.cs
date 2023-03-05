using Fulcrum.Services.CurrentConfig.Types;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Services.CurrentConfig;

internal partial class CurrentConfigService : ICurrentConfig
{
    public async Task<FetchConfig.FetchConfigResponse> FetchConfig()
    {
        var config = await _context.SysConfiguration
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (config == null)
        {
            return new FetchConfig.FetchConfigResponse
            {
                Message = "Failed to fetch current config",
                Status = false
            };
        }

        return new FetchConfig.FetchConfigResponse
        {
            Message = "Successfully got current config",
            Status = true,
            CurrentConfig = config
        };
    }
}