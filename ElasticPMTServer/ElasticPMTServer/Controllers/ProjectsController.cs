using ElasticPMTServer.Models;
using ElasticPMTServer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IRepository<Project> _projectRepository;

        public ProjectsController(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // GET: projects
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var result = _projectRepository.getAll().Hits;
            return Ok(result);
        }

        // GET: projects/{id}
        [HttpGet("{id}")]
        public IActionResult GetProjectById(string id)
        {
            var result = _projectRepository.getById(id);
            return Ok(result);
        }

        // POST: projects
        [HttpPost]
        public IActionResult createProject([FromBody] Project project)
        {
            var result = _projectRepository.create(project);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        // PUT: projects/{id}
        [HttpPut("{id}")]
        public IActionResult updateProject(string id, [FromBody] Project project)
        {
            var result = _projectRepository.update(id, project);
            return Ok(result);
        }

        // DELETE: projects/{id}
        [HttpDelete("{id}")]
        public IActionResult deleteProject(string id)
        {
            var result = _projectRepository.delete(id);
            return Ok(result);
        }
    }
}
