using System;
using Achievo.Shared;

namespace Achievo.Infrastructure.Models.Models;

public class UserTask
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public Reference? TaskTemplate { get; set; }
    public DateTime AssignedAt { get; set; }
    public DateTime DueAt { get; set; }
    public string? Status { get; set; }
    public DateTime CompletedAt { get; set; }
}
