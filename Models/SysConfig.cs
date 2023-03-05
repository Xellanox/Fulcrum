using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fulcrum.Models;

public class SysConfig
{
    [Key]
    public int SysConfigId {get; set;}

    public string LibraryBasePath {get; set;}
    public string ImportBasePath {get; set;}
    public string ArtBasePath {get; set;}
    
    public string LibraryStructureScheme {get; set;}

    [DefaultValue(false)]
    public bool FirstRunComplete {get; set;}
}