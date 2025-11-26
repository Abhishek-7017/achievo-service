using System;
using System.Threading.Tasks;
using Achievo.Contracts.Dto;
using Achievo.Infrastructure.DbContexts;
using Achievo.Infrastructure.Models.Models;
using Achievo.Services.Interfaces;
using Achievo.Shared;

namespace Achievo.Services.Services;

public class TaskService : ITaskService
{
    private readonly AchievoDbContext _achevoDbContext;
    private readonly IUserService _userService;
    public TaskService(AchievoDbContext achievoDbContext, IUserService userService)
    {
        _achevoDbContext = achievoDbContext;
        _userService = userService;
    }

    public TaskTemplateDto? CreateTaskTemplate(TaskTemplateDto taskTemplateDto)
    {
        if (taskTemplateDto is null) return null;

        var template = _achevoDbContext.TaskTemplates.FirstOrDefault(temp => temp.Title == taskTemplateDto.Title);
        if (template is not null)
        {
            return null;
        }

        TaskTemplate taskTemplate = new TaskTemplate
        {
            Title = taskTemplateDto.Title,
            Description = taskTemplateDto.Description,
            Difficulty = taskTemplateDto.Difficulty,
            Points = taskTemplateDto.Points,
            EstimatedDuration = taskTemplateDto.EstimatedDuration
        };

        _achevoDbContext.TaskTemplates.Add(taskTemplate);
        _achevoDbContext.SaveChanges();

        return taskTemplateDto;
    }

    public async Task<UserTask?> AssignTask(UserTaskDto userTask, Guid userId)
    {
        if ((userTask is null) || (userId == Guid.Empty))
        {
            return null;
        }
        User? user = await _userService.GetUserById(userId);
        if (user is null)
        {
            return null;
        }

        UserTask? tsk = await _achevoDbContext.UserTasks.FindAsync(userTask.Title);
        if (tsk is null)
        {
            return null;
        }

        Reference userRef = new Reference
        {
            Id = user.Id,
            Name = user.UserName
        };
        tsk.AssignedUser = userRef;
        tsk.AssignedUserId = user.Id;
        tsk.AssignedUserName = user.UserName;

        _achevoDbContext.UserTasks.Update(tsk);
        await _achevoDbContext.SaveChangesAsync();

        return tsk;
    }

    public async Task<UserTaskDto?>CreateTask(UserTaskDto userTaskDto)
    {
        if(userTaskDto is null ||Guid.Empty == userTaskDto.TemplateId) return null;

        var taskTemplate = await _achevoDbContext.TaskTemplates.FindAsync(userTaskDto.TemplateId);

        if(taskTemplate is null) return null;

        var userTask = await _achevoDbContext.UserTasks.FindAsync(userTaskDto.Title);

        if(userTask is not null) return null;

        Reference tempelateRef = new Reference
        {
            Id = taskTemplate.Id,
            Name = taskTemplate.Title
        };

        UserTask newUserTask = new UserTask{
            Title = userTaskDto.Title,
            Description = userTaskDto.Description,
            TaskTemplate = tempelateRef
        };
        newUserTask.AssignedAt = userTaskDto.AssignedAt;
        newUserTask.DueAt = userTaskDto.DueAt;
        newUserTask.Status = userTaskDto.Status;

        _achevoDbContext.UserTasks.Add(newUserTask);
        await _achevoDbContext.SaveChangesAsync();

        return userTaskDto;
    }

    public async Task<UserTaskDto?> UpdateStatusOfTask(UserTaskDto userTaskDto, string status)
    {
        if(userTaskDto is null) return null;

        var userTask = await _achevoDbContext.UserTasks.FindAsync(userTaskDto.Title);
        if(userTask is null) return null;

        userTask.Status = status;

        _achevoDbContext.UserTasks.Update(userTask);
        await _achevoDbContext.SaveChangesAsync();

        return userTaskDto;
    }

    public async Task<UserTaskDto?> UpdateUserTask(UserTaskDto userTaskDto)
    {
        if(userTaskDto is null) return null;

        var userTask = await _achevoDbContext.UserTasks.FindAsync(userTaskDto.Title);
        if(userTask is null) return null;

        userTask.Title = userTaskDto.Title;
        userTask.Description = userTaskDto.Description;
        userTask.AssignedAt = userTaskDto.AssignedAt;
        userTask.DueAt = userTaskDto.DueAt;
        userTask.Status = userTaskDto.Status;

        return userTaskDto;
    }
}
