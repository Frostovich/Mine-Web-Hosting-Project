namespace Full_proj.DtoModels;

public class MessageHistoryDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
    public string SenderId { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty;
    public bool IsMine { get; set; }  // удобно для фронта
}