using System;
using System.Diagnostics.CodeAnalysis;
using Achievo.Shared;

namespace Achievo.Infrastructure.Models.Models;

public class UserTask
{
    // [SetsRequiredMembers]
    // public UserTask(string title, string description, Reference taskTemplate)
    // {
    //     Title = title;
    //     Description = description;
    //     TaskTemplate = taskTemplate;
    // }
    public Guid Id { get; set; }
    public Reference AssignedUser { get; set; } = new Reference();
    public required Reference TaskTemplate { get; set; } = new Reference();
    public Guid AssignedUserId { get; set; } 
    public string AssignedUserName { get; set; } = string.Empty;
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime AssignedAt { get; set; }
    public DateTime DueAt { get; set; }
    public string? Status { get; set; }
    public DateTime CompletedAt { get; set; }
    public int? pointsAwarded { get; set; }
}
