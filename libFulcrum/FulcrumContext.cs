using Microsoft.EntityFrameworkCore;
using libFulcrum.Models;

namespace libFulcrum;

public class FulcrumContext : DbContext
{
    public FulcrumContext(DbContextOptions<FulcrumContext> options) : base(options)
    {
    }

    public DbSet<Mediafile> Mediafiles {get; set;}
    public DbSet<Playlist> Playlists {get; set;}
    public DbSet<PlaylistEntry> PlaylistEntries {get; set;}
}