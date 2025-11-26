using Achievo.Contracts.Dto;
using Achievo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AchievoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskService _taskService;
        private ILogger<TaskController> _logger;
        public TaskController(ITaskService taskService,ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("createtemplate")]
        public ActionResult<TaskTemplateDto> CreateTaskTemplate(TaskTemplateDto taskTemplateDto)
        {
            try
            {    
                var taskTemplate = _taskService.CreateTaskTemplate(taskTemplateDto);
                if (taskTemplate is null) return NotFound();
                return Ok(taskTemplate);
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("createtask")]
        public ActionResult<UserTaskDto> CreateTask(UserTaskDto userTaskDto)
        {
            try
            {
                var userTask = _taskService.CreateTask(userTaskDto);
                if(userTask is not null) return Ok(userTask);
                else return NotFound();
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("updatestatus")]
        public ActionResult<UserTaskDto> UpdateTaskStatus(UserTaskDto userTaskDto,string status)
        {
            try
            {
                var userTask = _taskService.UpdateStatusOfTask(userTaskDto,status);
                if(userTask is not null) return Ok(userTask);
                else return NotFound();
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        [Authorize]
        [HttpPut("updatetask")]
        public ActionResult<UserTaskDto> UpdateTask(UserTaskDto userTaskDto)
        {
            try
            {
                var userTask = _taskService.UpdateUserTask(userTaskDto);
                if(userTask is not null) return Ok(userTask);
                else return NotFound();
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("AssignTask")]
        public async Task<IActionResult> AssignTask(UserTaskDto userTask, Guid userId)
        {
            try
            {    
                var task = await _taskService.AssignTask(userTask,userId);
                if(task is not null) return Ok(task);
                return NotFound();
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
