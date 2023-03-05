using Fulcrum.Services.CurrentUser;

namespace Fulcrum.Services.UserManagement;

internal partial class UserManagementService : IUserManagement
{
    private readonly FulcrumContext _context;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICurrentUser _currentUser;

    public UserManagementService(FulcrumContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ICurrentUser currentUser)
    {
        _context = context;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _currentUser = currentUser;
    }
}