using System;
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository.IRepository;

namespace ApiEcommerce.Repository;

public class UserRepository : IUserRepository
{
    public User GetUser(int userId)
    {
        throw new NotImplementedException();
    }

    public ICollection<User> GetUsers()
    {
        throw new NotImplementedException();
    }

    public bool IsUniqueUser(string userName)
    {
        throw new NotImplementedException();
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
