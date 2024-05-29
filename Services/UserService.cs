using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;

public class UserService
{
    private readonly List<User> _users = new List<User>();

    public List<User> GetAllUsers()
    {
        return _users;
    }

    public User GetUserById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public void CreateUser(User user)
    {
        _users.Add(user);
    }

    public void UpdateUser(int id, User user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == id);
        if (existingUser != null)
        {
            existingUser.Name = user.Name;
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
        }
    }

    public void DeleteUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _users.Remove(user);
        }
    }
}
