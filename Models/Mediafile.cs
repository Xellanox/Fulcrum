using System.ComponentModel.DataAnnotations;

namespace Fulcrum.Models;

public class Mediafile
{
    [Key]
    public int MediafileId {get; set;}

    public string Title {get; set;}
    public string Artist {get; set;}
    public string AlbumArtist {get; set;}
    public string Album {get; set;}

    public string Comment {get; set;}

    public int Year {get; set;}
    public int TrackNumber {get; set;}
    public int DiscNumber { get; set; }

    public string FilePath {get; set;}
    public string Filename {get; set;}

    public string ArtPath {get; set;}
    public string ArtFilename {get; set;}
    
    public string MimeType {get; set;}

    public string Codec {get; set;}
    public int Bitrate {get; set;}
    public int Samplerate {get; set;}

    public DateTime? AddedDateTime {get; set;}
    public DateTime? LastPlayed {get; set;}

    public int PlayCount {get; set;}

    public int ImportedByUserId {get; set;}

    public DateTime LastModified { get; set; }
}