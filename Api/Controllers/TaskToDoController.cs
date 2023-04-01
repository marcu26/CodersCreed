using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tasks")]
    public class TaskToDoController : Controller
    {
        public TaskToDoService _taskToDoService { get; set; }
        public TaskToDoController(TaskToDoService taskToDoService)
        {
            _taskToDoService = taskToDoService;
        }
    }
}
