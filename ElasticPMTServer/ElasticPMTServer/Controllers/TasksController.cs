using ElasticPMTServer.Models;
using ElasticPMTServer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET: tasks
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var result = _taskRepository.getTasks().Hits;
            return Ok(result);
        }

        // GET: tasks/{id}
        [HttpGet("{id}")]
        public IActionResult GetTaskById(string id)
        {
            var result = _taskRepository.getTaskById(id);
            return Ok(result);
        }

        // POST: tasks
        [HttpPost]
        public IActionResult createTask([FromBody] Task task)
        {
            var result = _taskRepository.createTask(task);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        // PUT: tasks/{id}
        [HttpPut("{id}")]
        public IActionResult updateTask(string id, [FromBody] Task task)
        {
            var result = _taskRepository.updateTask(id, task);
            return Ok(result);
        }

        // DELETE: tasks/{id}
        [HttpDelete("{id}")]
        public IActionResult deleteTask(string id)
        {
            var result = _taskRepository.deleteTask(id);
            return Ok(result);
        }
    }
}
