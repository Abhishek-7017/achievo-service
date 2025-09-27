using System;
using Achievo.Infrastructure.Models.Models;

namespace Auth.Service.Interface;

public interface IAuthService
{
    public Task<User?> RegisterUser(UserDto)
}
