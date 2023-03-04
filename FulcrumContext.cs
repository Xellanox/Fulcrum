using Microsoft.EntityFrameworkCore;
using Fulcrum.Models;

namespace Fulcrum;

public class FulcrumContext : DbContext
{
    public FulcrumContext(DbContextOptions<FulcrumContext> options) : base(options)
    {
    }

    public DbSet<User> Users {get; set;}
    public DbSet<Session> Sessions {get; set;}
}