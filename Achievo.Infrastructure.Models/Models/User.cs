using System;
using System.ComponentModel;
using Achievo.Shared;

namespace Achievo.Infrastructure.Models.Models;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public required string DisplayName { get; set; }
    public required DateTime JoiningDate { get; set; }
    public bool IsActive { get; set; }
    public int TotalPoints { get; set; }
    public List<string>? Role { get; set; }
    public List<Reference>? AssignedTasks { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
