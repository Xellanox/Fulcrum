using System.ComponentModel.DataAnnotations;

namespace libFulcrum.Models;

public class PlaylistEntry
{
    [Key]
    public int PlaylistEntryId {get; set;}
    public int Position {get; set;}
    public DateTime DateAdded {get; set;}

    // Relationships
    public int PlaylistId {get; set;}
    public Playlist Playlist {get; set;}
    public int MediafileId {get; set;}
    public Mediafile Mediafile {get; set;}
}