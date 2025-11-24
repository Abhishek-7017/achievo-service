using System;
using Achievo.Contracts.Dto;
using Achievo.Infrastructure.Models.Models;

namespace Achievo.Services.Interfaces;

public interface ITaskService
{
    Task<UserTaskDto?> CreateTask(Guid templateId,UserTaskDto userTaskDto);
    UserTaskDto UpdateUserTask(UserTaskDto userTask);
    UserTaskDto UpdateStatusOfTask(UserTaskDto userTask, string status);
    Task<UserTask?> AssignTask(UserTaskDto userTask, Guid userId);
    TaskTemplateDto? CreateTaskTemplate(TaskTemplateDto taskTemplateDto);
}
