using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
namespace Full_proj.Controllers;
using Microsoft.AspNetCore.Mvc;
using Full_proj.Domain_Models;
using Full_proj.DbContext;
[ApiController]
[Route("api/History") ]
public  class HistoryController : ControllerBase
{
    private readonly ILogger<HistoryController> _logger;
    private readonly DataBase _database;
    public HistoryController(ILogger<HistoryController>? logger, DataBase database)
    {
        _logger = logger;
        _database = database;
    }
    [HttpGet("history/{withUserId:int}")]
    public async Task<IActionResult> GetHistory(int withUserId)
    {
        // Получаем текущего пользователя из JWT
        var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(currentUserIdClaim))
            return Unauthorized();
    
        int currentUserId = int.Parse(currentUserIdClaim); // конвертируем строку в int

        var messages = await _database.Message
            .Where(m => (m.SenderId == currentUserId && m.ReceiverId == withUserId) ||
                        (m.SenderId == withUserId && m.ReceiverId == currentUserId))
            .OrderBy(m => m.Date)
            .Take(50)
            .ToListAsync();

        return Ok(messages);
    }
}