using ElasticPMTServer.Models;
using ElasticPMTServer.Repositories;
using ElasticPMTServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrioritiesController : ControllerBase
    {
        private readonly IPriorityRepository _priorityRepository;
        private readonly ISearchService _searchService;

        public PrioritiesController(IPriorityRepository priorityRepository, ISearchService searchService)
        {
            _priorityRepository = priorityRepository;
            _searchService = searchService;
        }

        // GET: priorities
        [HttpGet]
        public IActionResult GetAllPriorities()
        {
            var result = _priorityRepository.getAll().Hits;
            return Ok(result);
        }

        // GET: priorities/{id}
        [HttpGet("{id}")]
        public IActionResult GetPriorityById(string id)
        {
            var result = _priorityRepository.getById(id);
            return Ok(result);
        }

        // GET: priorities/{id}
        [HttpPost("search")]
        public IActionResult Search([FromBody] SearchObject searchValue)
        {
            var result = _searchService.Autocomplete(searchValue.Query, 5);
            return Ok(result);
        }

        // POST: priorities
        [HttpPost]
        public IActionResult createPriority([FromBody] Priority priority)
        {
            var result = _priorityRepository.create(priority);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        // PUT: priorities/{id}
        [HttpPut("{id}")]
        public IActionResult updatePriority(string id, [FromBody] Priority priority)
        {
            var result = _priorityRepository.update(id, priority);
            return Ok(result);
        }

        // DELETE: priorities/{id}
        [HttpDelete("{id}")]
        public IActionResult deletePriority(string id)
        {
            var result = _priorityRepository.delete(id);
            return Ok(result);
        }
    }
}
