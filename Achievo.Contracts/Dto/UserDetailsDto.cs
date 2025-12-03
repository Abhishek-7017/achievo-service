using System;

namespace Achievo.Contracts.Dto;

public class UserDetailsDto
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; }
    public DateTime JoiningDate { get; set; }
    public bool IsActive { get; set; }
    public int TotalPoints { get; set; }
    public List<string>? Role { get; set; }
}
