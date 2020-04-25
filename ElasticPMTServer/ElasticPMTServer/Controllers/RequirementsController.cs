using ElasticPMTServer.Models;
using ElasticPMTServer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequirementsController : ControllerBase
    {

        private readonly IRequirementRepository _requirementRepository;

        public RequirementsController(IRequirementRepository requirementRepository)
        { 
            _requirementRepository = requirementRepository;
        }

        // GET: requirements
        [HttpGet]
        public IActionResult GetAllRequirements()
        {
            var result = _requirementRepository.getRequirements().Hits;
            return Ok(result);
        }

        // GET: requirements/{id}
        [HttpGet("{id}")]
        public IActionResult GetRequirementById(string id)
        {
            var result = _requirementRepository.getRequirementById(id);
            return Ok(result);
        }

        // POST: requirements
        [HttpPost]
        public IActionResult createRequirement([FromBody] Requirement requirement)
        {
            var result = _requirementRepository.createRequirement(requirement);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        // PUT: requirements/{id}
        [HttpPut("{id}")]
        public IActionResult updateRequirement(string id, [FromBody] Requirement requirement)
        {
            var result = _requirementRepository.updateRequirement(id, requirement);
            return Ok(result);
        }

        // DELETE: requirements/{id}
        [HttpDelete("{id}")]
        public IActionResult deleteRequirement(string id)
        {
            var result = _requirementRepository.deleteRequirement(id);
            return Ok(result);
        }
    }
}
