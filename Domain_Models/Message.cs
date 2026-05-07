namespace Full_proj.Domain_Models;

public class Messages
{
    public int Id { get; set; }          // ← первичный ключ
    public string? Message { get; set; } // ← текст сообщения
    public int SenderId { get; set; }
    public User? Sender { get; set; }
    public int ReceiverId { get; set; }
    public User? Receiver { get; set; }
    public DateTime Date { get; set; }    // ← дата отправки
    public int? UserId { get; set; }      // ← внешний ключ (кто отправил)
    public User? User { get; set; }       // ← навигационное свойство
}