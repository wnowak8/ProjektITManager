using Backend.DbServices;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService taskService;

        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await taskService.GetAllTasks();
            return StatusCode(StatusCodes.Status200OK, tasks);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> AddTask(TaskModel task)
        {
            var dbTask = await taskService.AddTask(task);

            return StatusCode(StatusCodes.Status200OK, task);

        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskModel>> GetTaskById(Guid taskId)
        {
            var task = await taskService.GetTaskById(taskId);

            if (task == null)
                return NotFound();

            return StatusCode(StatusCodes.Status200OK, task);
        }

        [HttpPut("{taskId}")]
        public async Task<ActionResult<TaskModel>> UpdateTask(Guid taskId, TaskModel updatedTask)
        {
            if (taskId != updatedTask.TaskId)
                return BadRequest();

            var existingTask = await taskService.UpdateTask(updatedTask);

            if (existingTask == null)
                return NotFound();

            return StatusCode(StatusCodes.Status200OK, existingTask);
        }

        [HttpDelete("{taskId}")]
        public async Task<ActionResult<TaskModel>> DeleteTask(Guid taskId)
        {
            var deletedTask = await taskService.DeleteTask(taskId);

            if (deletedTask == null)
                return NotFound();

            return StatusCode(StatusCodes.Status200OK, deletedTask);
        }
    }
}
