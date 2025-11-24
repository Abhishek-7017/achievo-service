using System;
using Achievo.Contracts.Dto;
using Achievo.Infrastructure.Models.Models;

namespace Achievo.Services.Interfaces;

public interface IUserService
{
    public Task<UserDto?> UpdateUser(UserDto userDto);
    public List<UserDto> GetAllUsers();
    public Task<User?> GetUserById(Guid Id);
    public Task<UserDto?> GetUserDtoById(Guid Id);
}
