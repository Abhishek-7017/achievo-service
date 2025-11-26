using System;
using Achievo.Contracts.Dto;
using Achievo.Infrastructure.Models.Models;

namespace Achievo.Services.Interfaces;

public interface ITaskService
{
    Task<UserTaskDto?> CreateTask(UserTaskDto userTaskDto);
    Task<UserTaskDto?> UpdateUserTask(UserTaskDto userTask);
    Task<UserTaskDto?> UpdateStatusOfTask(UserTaskDto userTask, string status);
    Task<UserTask?> AssignTask(UserTaskDto userTask, Guid userId);
    TaskTemplateDto? CreateTaskTemplate(TaskTemplateDto taskTemplateDto);
}
