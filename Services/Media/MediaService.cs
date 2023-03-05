using Fulcrum.Services.CurrentConfig;
using Fulcrum.Services.CurrentUser;

namespace Fulcrum.Services.Media;

internal partial class MediaService : IMedia
{
    private readonly FulcrumContext _context;
    private readonly ICurrentUser _currentUser;
    private readonly ICurrentConfig _currentConfig;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MediaService(FulcrumContext context, ICurrentUser currentUser, ICurrentConfig currentConfig, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _currentUser = currentUser;
        _currentConfig = currentConfig;
        _httpContextAccessor = httpContextAccessor;
    }
}