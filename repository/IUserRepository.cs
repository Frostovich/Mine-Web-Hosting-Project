using Microsoft.EntityFrameworkCore;

namespace Full_proj.repository;

using Full_proj.DbContext;

using Full_proj.Domain_Models;

public interface IUserRepository
{
    public Task<User> AddUser(string username, string password, string email);
    public Task<User> GetUserByUsername(string username);

}

public class UserRepository : IUserRepository
{
    private readonly DataBase _dataBase;
    public UserRepository(DataBase dataBase)
    {
        _dataBase = dataBase;
    }

    public Task<User> AddUser(string username, string password, string email)
    {
        var user = new User
        {
            Username = username,
            Password = password,
            Email = email
        };
        
        _dataBase.Add(user);
        _dataBase.SaveChangesAsync();
        return Task.FromResult(user);
    }

    public async Task<User> GetUserByUsername(string username)
    {
         return await _dataBase.Set<User>().FirstOrDefaultAsync(u => u.Username == username);
    }
    
}