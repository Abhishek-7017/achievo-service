using System;
using Achievo.Contracts.Dto;
using Achievo.Infrastructure.Models.Models;

namespace Achievo.Services.Interfaces;

public interface IAuthService
{
    Task<User?> RegisterUserAsync(UserDto request);
    Task<TokenResponseDto?> LoginUserAsync(UserDto request);
    Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
}
