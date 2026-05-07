using Full_proj.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Full_proj.Domain_Models;
namespace Full_proj.ChatHub;
[Authorize]
public class Chathub : Hub
{
    private readonly DataBase _dataBase;
    private readonly ILogger <Chathub> _logger;
    public Chathub(DataBase db, ILogger <Chathub> logger)
    {
        _dataBase = db;
        _logger = logger;
    }
    public async Task SendMessage(int userId, string message)  // ← int
    {
        var idUser = Context.UserIdentifier;  // это string (Id текущего пользователя)
    
        // Отправляем получателю (Clients.User() принимает string, поэтому конвертируем)
        await Clients.User(userId.ToString()).SendAsync("ReceiveMessage", idUser, message);
    
        var msg = new Messages
        {
            Date = DateTime.Now,
            UserId = userId,    // ← int
            Message = message,
        };
        try
        {
            _dataBase.Message.Add(msg);
            await _dataBase.SaveChangesAsync();
        }
        catch 
        {
            _logger.LogError($"An error occured while sending a message: {message}");
        }
    }
    
    
}