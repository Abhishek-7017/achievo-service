using System;
using System.Threading.Tasks;
using Achievo.Contracts.Dto;
using Achievo.Infrastructure.DbContexts;
using Achievo.Infrastructure.Models.Models;
using Achievo.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Achievo.Services.Services;

public class UserService : IUserService
{
    public UserService(AchievoDbContext achievoDbContext)
    {
        _achievoDbContext = achievoDbContext;
    }
    private AchievoDbContext _achievoDbContext;

    public async Task<User?> GetUserById(Guid Id)
    {
        var user = await _achievoDbContext.Users.FindAsync(Id);
        if (user is null) {
            return null;
        }

        return user;
    }

    public async Task<UserDetailsDto?> UpdateUser(UserDetailsDto userDetailsDto)
    {
        User? user = await _achievoDbContext.Users.FirstOrDefaultAsync(u=>u.UserName==userDetailsDto.UserName);
        if (user is null)
        {
            return null;
        }
        user.DisplayName = userDetailsDto.DisplayName;
        user.Email = userDetailsDto.Email;
        user.JoiningDate = userDetailsDto.JoiningDate;
        user.Role = userDetailsDto.Role;
        user.IsActive = userDetailsDto.IsActive;
        user.TotalPoints = userDetailsDto.TotalPoints;

        _achievoDbContext.Users.Update(user);
        await _achievoDbContext.SaveChangesAsync();

        return userDetailsDto;
    }

    public async Task<UserDetailsDto?> GetUserByUserName(string userName)
    {
        User? user = await _achievoDbContext.Users.FirstOrDefaultAsync(u=>u.UserName==userName);
        if (user is null)
        {
            return null;
        }
        UserDetailsDto userDetailsDto = new UserDetailsDto
        {
            UserName = user.UserName,
            DisplayName = user.DisplayName,
            Email = user.Email,
            JoiningDate = user.JoiningDate,
            IsActive = user.IsActive,
            Role = user.Role,
            TotalPoints = user.TotalPoints
        };

        return userDetailsDto;
    }
}
