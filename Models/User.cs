using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fulcrum.Models;

public class User
{
    [Key]
    public int UserId {get; set;}

    public string Firstname {get; set;}
    public string Lastname {get; set;}

    public string Email {get; set;}
    public string Username {get; set;}
    [JsonIgnore]
    public string Password {get; set;}

    public bool IsAdmin {get; set;}
    public bool IsEnabled {get; set;}

    public DateTime? Registration {get; set;}
    public DateTime? LastLogin {get; set;}

    // Relationships
    public virtual List<Session> UserSessions {get; set;}
}