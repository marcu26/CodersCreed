using Core.Dtos.Tasks;
using Core.Services;
using Infrastructure.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tasks")]
    public class TaskToDoController : BaseController
    {
        public TaskToDoService _taskToDoService { get; set; }
        public TaskToDoController(TaskToDoService taskToDoService)
        {
            _taskToDoService = taskToDoService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateTask([FromBody] CreateTaskToDoDto payload)
        {
            await _taskToDoService.CreateTaskToDoAsync(payload);
            return Ok();
        }

        [HttpGet("get-one/{taskId}")]
        public async Task<ActionResult> GetTask([FromRoute] int taskId)
        {
            var task = await _taskToDoService.GetTaskById(taskId);
            return Ok(task);
        }

        [HttpPut("assign/{taskId}-{userId}")]
        public async Task<ActionResult> AssignTaskToUser([FromRoute] int taskId, [FromRoute]int userId)
        {
            await _taskToDoService.AssignTaskToUser(taskId,userId);
            return Ok();
        }

        [HttpPut("edit/{taskId}")]
        public async Task<ActionResult> EditTask([FromRoute]int taskId,[FromBody] EditTaskToDoDto payload)
        {
            await _taskToDoService.EditTask(taskId,payload);
            return Ok();
        }

        [HttpPut("mark/{taskId}")]
        public async Task<ActionResult> MarkTaskAsDone([FromRoute] int taskId)
        {
            await _taskToDoService.MarkTask(taskId);
            return Ok();
        }

        [HttpPut("unmark/{taskId}")]
        public async Task<ActionResult> UnmarkTask([FromRoute] int taskId)
        {
            await _taskToDoService.UnmarkTask(taskId);
            return Ok();
        }

        [HttpPut("delete/{taskId}")]
        public async Task<ActionResult> DeleteTask([FromRoute] int taskId)
        {
            await _taskToDoService.DeleteTask(taskId);
            return Ok();
        }

        [HttpPost("get-pagina")]
        public async Task<ActionResult> GetTableModesPaginaAsync([FromBody] PageablePostModelTaskToDo payload)
        {
            try
            {
                var dto = await _taskToDoService.GetTasksToDoDtoPaginaAsync(payload);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
