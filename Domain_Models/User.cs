namespace Full_proj.Domain_Models;

public class User
{
    public int userId { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public List<Messages>? Messages { get; set; } 
    public ICollection<Messages> SentMessages { get; set; } = new List<Messages>();
    
    // Полученные сообщения
    public ICollection<Messages> ReceivedMessages { get; set; } = new List<Messages>();
    public ICollection<Contacts> OwnedContacts { get; set; } = new List<Contacts>();
    public ICollection<Contacts> ContactOfOthers { get; set; } = new List<Contacts>();
}