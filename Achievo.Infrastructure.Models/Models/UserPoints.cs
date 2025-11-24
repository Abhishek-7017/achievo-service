using System;

namespace Achievo.Infrastructure.Models.Models;

public class UserPoints
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int Points { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Reason { get; set; } = string.Empty;
}
