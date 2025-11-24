using System;

namespace Achievo.Contracts.Dto;

public class UserTaskDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public Guid TemplateId { get; set; }
    public DateTime AssignedAt { get; set; }
    public DateTime DueAt { get; set; }
    public string? Status { get; set; }
}
