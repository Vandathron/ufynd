using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ufynd.Infrastructure.Services;

namespace Ufynd.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("task-one")]
        public IActionResult TaskOne()
        {
            _taskService.TaskOne();
            return Ok();
        }

        [HttpGet("task-two")]
        public async Task<IActionResult> TaskTwo()
        {
            await _taskService.TaskTwo();
            return Ok();
        }

        [HttpGet("task-three")]
        public IActionResult TaskThree([FromQuery] int hotelId, [FromQuery] string arrivalDate)
        {
            var data =  _taskService.TaskThree(hotelId, arrivalDate);
            return data != null ? Ok(data) : NotFound("Not found");
        }
    }
}