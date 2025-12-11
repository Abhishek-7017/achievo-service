using System;
using Achievo.Contracts.Dto;
using Achievo.Infrastructure.Models.Models;

namespace Achievo.Services.Interfaces;

public interface IUserService
{
    public Task<UserDetailsDto?> UpdateUser(UserDetailsDto userDetailsDto);
    public Task<User?> GetUserById(Guid Id);
    public Task<UserDetailsDto?> GetUserByUserName(string UserName);
}
