using System;

namespace Achievo.Infrastructure.Models.Models;

public class TaskTemplate
{
    public TaskTemplate(string title, string description)
    {
        Title = title;
        Description = description;
    }
    public TaskTemplate(){}
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int Points { get; set; }
    public string Frequency { get; set; } = string.Empty;
    public int? Difficulty { get; set; }
    public TimeSpan? EstimatedDuration { get; set; }
}
