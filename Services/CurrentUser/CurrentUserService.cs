namespace Fulcrum.Services.CurrentUser;

internal partial class CurrentUserService : ICurrentUser
{
    private readonly FulcrumContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(FulcrumContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
}