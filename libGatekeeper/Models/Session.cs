using System.ComponentModel.DataAnnotations;

namespace libGatekeeper.Models;

public class Session
{
    [Key]
    public int SessionId {get; set;}

    public string Token {get; set;}
    
    public DateTime Issued {get; set;}
    public DateTime Expires {get; set;}

    public string UserAgent {get; set;}
    public string IP {get; set;}

    // Relationships
    public int UserId {get; set;}
    public virtual User UserDetails {get; set;}
}