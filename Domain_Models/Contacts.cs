namespace Full_proj.Domain_Models;

public class Contacts
{
    public int Id { get; set; }
    public int OwnerId { get; set; }      // ← был string, стал int
    public int ContactId { get; set; }    // ← был string, стал int
    public string Name { get; set; }
    public string Profile { get; set; }
    public int PhoneNumber { get; set; }
    
    public User? Owner { get; set; }
    public User? Contact { get; set; }
}