using System;

namespace Achievo.Contracts.Dto;

public class TaskTemplateDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int Points { get; set; }
    public int Difficulty { get; set; }
    public TimeSpan EstimatedDuration { get; set; }
}
