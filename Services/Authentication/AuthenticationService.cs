namespace Fulcrum.Services.Authentication;

internal partial class AuthenticationService : IAuthentication
{
    private readonly FulcrumContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public AuthenticationService(FulcrumContext context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }
}