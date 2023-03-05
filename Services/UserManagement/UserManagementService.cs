namespace Fulcrum.Services.UserManagement;

internal partial class UserManagementService : IUserManagement
{
    private readonly FulcrumContext _context;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserManagementService(FulcrumContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }
}