using ElasticPMTServer.Models;
using ElasticPMTServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            var result = _requirementRepository.getAll().Hits;
            return Ok(result);
        }

        // GET: requirements/{id}
        [HttpGet("{id}")]
        public IActionResult GetRequirementById(string id)
        {
            var result = _requirementRepository.getById(id);
            return Ok(result);
        }

        // POST: requirements
        [HttpPost]
        public IActionResult createRequirement([FromBody] Requirement requirement)
        {
            var result = _requirementRepository.create(requirement);
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
            var result = _requirementRepository.update(id, requirement);
            return Ok(result);
        }

        // PUT: requirements/comments/{id}
        [HttpPut("comments/{id}")]
        public IActionResult updateComments(string id, [FromBody] List<Comment> comments)
        {
            var result = _requirementRepository.updateComments(id, comments);
            return Ok(result);
        }

        // DELETE: requirements/{id}
        [HttpDelete("{id}")]
        public IActionResult deleteRequirement(string id)
        {
            var result = _requirementRepository.delete(id);
            return Ok(result);
        }
    }
}
