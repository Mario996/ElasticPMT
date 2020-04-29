using ElasticPMTServer.Models;
using ElasticPMTServer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // GET: projects
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var result = _projectRepository.getProjects().Hits;
            return Ok(result);
        }

        // GET: projects/{id}
        [HttpGet("{id}")]
        public IActionResult GetProjectById(string id)
        {
            var result = _projectRepository.getProjectById(id);
            return Ok(result);
        }

        // POST: projects
        [HttpPost]
        public IActionResult createProject([FromBody] Project project)
        {
            var result = _projectRepository.createProject(project);
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
            var result = _projectRepository.updateProject(id, project);
            return Ok(result);
        }

        // DELETE: projects/{id}
        [HttpDelete("{id}")]
        public IActionResult deleteProject(string id)
        {
            var result = _projectRepository.deleteProject(id);
            return Ok(result);
        }
    }
}
