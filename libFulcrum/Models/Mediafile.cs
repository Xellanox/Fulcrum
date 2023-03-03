using System.ComponentModel.DataAnnotations;

namespace libFulcrum.Models;

public class Mediafile
{
    [Key]
    public int MediafileId {get; set;}
    public string Title {get; set;}
    public string AlbumArtist {get; set;}
    public string AlbumTitle {get; set;}
    public string TrackArtist {get; set;}
    public int TrackNumber {get; set;}
    public int DiscNumber {get; set;}
    public int Year {get; set;}
    public string Genre {get; set;}
    public string Path {get; set;}
    public string Filename {get; set;}
    public int BitRate {get; set;}
    public int SampleRate {get; set;}
    public string Codec {get; set;}
    public int Duration {get; set;}
    public int PlayCount {get; set;}
    public int Rating {get; set;}
    public DateTime LastPlayed {get; set;}
    public DateTime DateAdded {get; set;}
}