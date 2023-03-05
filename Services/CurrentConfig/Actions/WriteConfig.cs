using System.Runtime.Versioning;
using Fulcrum.Models;
using Fulcrum.Services.CurrentConfig.Types;

namespace Fulcrum.Services.CurrentConfig;

internal partial class CurrentConfigService : ICurrentConfig
{
    public async Task<WriteConfig.WriteConfigResponse> WriteConfig(WriteConfig.WriteConfigRequest request)
    {
        var currentConfig = await FetchConfig();

        if (currentConfig == null || currentConfig.Status == false)
        {
            return new WriteConfig.WriteConfigResponse
            {
                Message = "Write failed, couldn't fetch current config",
                Status = false
            };
        }

        var updated = new SysConfig(){
            SysConfigId = currentConfig.CurrentConfig.SysConfigId,
            ArtBasePath = currentConfig.CurrentConfig.ArtBasePath != request.NewConfig.ArtBasePath
                ? request.NewConfig.ArtBasePath : currentConfig.CurrentConfig.ArtBasePath,

            FirstRunComplete = currentConfig.CurrentConfig.FirstRunComplete != request.NewConfig.FirstRunComplete
                ? request.NewConfig.FirstRunComplete : currentConfig.CurrentConfig.FirstRunComplete,

            ImportBasePath = currentConfig.CurrentConfig.ImportBasePath != request.NewConfig.ImportBasePath
                ? request.NewConfig.ImportBasePath : currentConfig.CurrentConfig.ImportBasePath,

            LibraryBasePath = currentConfig.CurrentConfig.LibraryBasePath != request.NewConfig.LibraryBasePath
                ? request.NewConfig.LibraryBasePath : currentConfig.CurrentConfig.LibraryBasePath,

            LibraryStructureScheme = currentConfig.CurrentConfig.LibraryStructureScheme != request.NewConfig.LibraryStructureScheme
                ? request.NewConfig.LibraryStructureScheme : currentConfig.CurrentConfig.LibraryStructureScheme
        };

        _context.Update<SysConfig>(updated);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new WriteConfig.WriteConfigResponse
            {
                Message = "Error occurred while writing config",
                Status = false
            };
        }

        return new WriteConfig.WriteConfigResponse
        {
            Message = "Successfully wrote config",
            Status = true
        };
    }
}