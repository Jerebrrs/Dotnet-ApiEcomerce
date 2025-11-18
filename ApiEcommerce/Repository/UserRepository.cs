using System;
using ApiEcommerce.Data;
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;

namespace ApiEcommerce.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public User? GetUser(int userId)
    {
        if (userId < 0) return null;
        return _db.Users.FirstOrDefault(u => u.Id == userId);
    }

    public ICollection<User> GetUsers()
    {
        return _db.Users.ToList();
    }

    public bool IsUniqueUser(string userName)
    {
        if (userName == "") return true;
        return !_db.Users.Any(u => u.UserName.ToLower().Trim() == userName.ToLower().Trim());
    }

    public Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
    {
        throw new NotImplementedException();
    }

    public Task<User> Register(CreateUserDto createUserDto)
    {
        throw new NotImplementedException();
    }
}
