using System.ComponentModel.DataAnnotations;

namespace libFulcrum.Models;

public class Playlist
{
    [Key]
    public int PlaylistId {get; set;}

    public string Title {get; set;}
    public string Description {get; set;}
    public string Image {get; set;}

    public int OwnerUserId {get; set;}
    public DateTime DateCreated {get; set;}
    public DateTime LastModified {get; set;}
    public bool IsPublic {get; set;}

    // Relationships
    public List<PlaylistEntry> PlaylistEntries {get; set;}
}