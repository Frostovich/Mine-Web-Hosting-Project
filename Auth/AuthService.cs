using Full_proj.Domain_Models;
using Microsoft.EntityFrameworkCore;

namespace Full_proj.Auth;
using Full_proj.DbContext;
using Full_proj.JWTService;
using Full_proj.repository;

public class AuthService
{
    private readonly DataBase _dataBase;
    private readonly IUserRepository _userRepository;
    private readonly AuService _auService;

    public AuthService(DataBase dataBase, IUserRepository userRepository, AuService auService)
    {
        _dataBase = dataBase;
        _userRepository = userRepository;
        _auService = auService;
    }

    public async Task<string> Register(string username, string password, string email)
    {
        var existing = _dataBase.Set<User>().FirstOrDefaultAsync(u => u.Username == username);
        if (existing != null) throw new Exception("This username already exists");
        var user = new User
        {
            Username = username,
            Password = password,
            Email = email
        };
        _dataBase.Set<User>().Add(user);
        await _dataBase.SaveChangesAsync();
        return _auService.GenerateToken(username);
    }

    public async Task<string> Login(string username, string password)
    {
        var user = await _dataBase.Set<User>().FirstOrDefaultAsync(u => u.Username == username);
        if (user == null || user.Password != password) throw new Exception("Invalid username or password");
        return _auService.GenerateToken(username);
    }
}








