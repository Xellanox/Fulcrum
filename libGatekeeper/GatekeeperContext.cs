using Microsoft.EntityFrameworkCore;
using libGatekeeper.Models;

namespace libGatekeeper;

public class GatekeeperContext : DbContext
{
    public GatekeeperContext(DbContextOptions<GatekeeperContext> options) : base(options) { }

    public DbSet<User> Users {get; set;}
    public DbSet<Session> Sessions {get; set;}
}