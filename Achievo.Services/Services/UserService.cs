using System;
using System.Threading.Tasks;
using Achievo.Contracts.Dto;
using Achievo.Infrastructure.DbContexts;
using Achievo.Infrastructure.Models.Models;
using Achievo.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Achievo.Services.Services;

public class UserService : IUserService
{
    public UserService(AchievoDbContext achievoDbContext)
    {
        _achievoDbContext = achievoDbContext;
    }
    private AchievoDbContext _achievoDbContext;
    public List<UserDto> GetAllUsers()
    {
        List<User> users = _achievoDbContext.Users.ToList();
        List<UserDto> userDtos = new List<UserDto>();
        foreach (var user in users)
        {
            userDtos.Add(new UserDto
            {
                UserName = user.UserName,
                Password = user.PasswordHash
            });
        }
        return userDtos;
    }

    public async Task<User?> GetUserById(Guid Id)
    {
        var user = await _achievoDbContext.Users.FindAsync(Id);
        if (user is null) {
            return null;
        }

        return user;
    }

    public async Task<UserDto?> UpdateUser(UserDto request)
    {
        User? user = await _achievoDbContext.Users.FindAsync(request.UserName);
        if (user is null)
        {
            return null;
        }
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);

        _achievoDbContext.Users.Update(user);
        await _achievoDbContext.SaveChangesAsync();

        return new UserDto { UserName = user.UserName, Password = user.PasswordHash };
    }

    public async Task<UserDto?> GetUserDtoById(Guid Id)
    {
        User? user = await _achievoDbContext.Users.FindAsync(Id);
        if (user is null)
        {
            return null;
        }
        UserDto userDto = new UserDto
        {
            UserName = user.UserName,
            Password = user.PasswordHash
        };
        
        return userDto;
    }
}
