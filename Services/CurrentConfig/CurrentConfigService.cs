namespace Fulcrum.Services.CurrentConfig;

internal partial class CurrentConfigService : ICurrentConfig
{
    private readonly FulcrumContext _context;

    public CurrentConfigService(FulcrumContext context)
    {
        _context = context;
    }
}