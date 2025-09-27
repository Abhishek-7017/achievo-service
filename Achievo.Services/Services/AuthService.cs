using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Achievo.Contracts.Dto;
using Achievo.Infrastructure.DbContexts;
using Achievo.Infrastructure.Models.Models;
using Achievo.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Achievo.Services.Services;

public class AuthService : IAuthService
{
    public AuthService(UserDbContext userDbContext, IConfiguration configuration)
    {
        _userDbContext = userDbContext;
        _configuration = configuration;
    }
    private UserDbContext _userDbContext;
    private IConfiguration _configuration;

    public async Task<TokenResponseDto?> LoginUserAsync(UserDto request)
    {
        User? user = await _userDbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
        if (user is null)
        {
            return null;
        }
        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
        {
            return null;
        }
        return await CreateResponseToken(user);
    }

    private async Task<TokenResponseDto> CreateResponseToken(User user)
    {
        return new TokenResponseDto
        {
            AccessToken = CreateToken(user),
            RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
        };
    }

    public async Task<User?> RegisterUserAsync(UserDto request)
    {
        if (await _userDbContext.Users.AnyAsync(u => u.UserName == request.UserName))
        {
            return null;
        }

        User user = new User();
        var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);

        user.UserName = request.UserName;
        user.PasswordHash = hashedPassword;

        _userDbContext.Add(user);
        await _userDbContext.SaveChangesAsync();

        return user;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private async Task<User?> ValidateUserTokenAsync(Guid userId, string refreshToken)
    {
        var user = await _userDbContext.Users.FindAsync(userId);
        if (user is null || user.RefreshToken != refreshToken ||
        user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return null;
        }
        return user;
    }

    public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
    {
        var user = await ValidateUserTokenAsync(request.UserId, request.RefreshToken);
        if (user is null)
        {
            return null;
        }
        return await CreateResponseToken(user);
    }

    private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
    {
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await _userDbContext.SaveChangesAsync();
        return refreshToken;
    }
    

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role)
            };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration["AppSettings:Issuer"],
            audience: _configuration["AppSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

}
