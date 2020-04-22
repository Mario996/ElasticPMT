using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticPMTServer.Models;
using ElasticPMTServer.Services;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequirementsController : ControllerBase
    {

        private readonly IElasticSearchService _elasticSearchService;

        public RequirementsController(IElasticSearchService elasticSearchService)
        { 
            _elasticSearchService = elasticSearchService;
        }

        // GET: requirements
        [HttpGet]
        public IActionResult GetAllRequirements()
        {
            var result = _elasticSearchService.getRequirements().Hits;
            return Ok(result);
        }

        // GET: requirements/{id}
        [HttpGet("{id}")]
        public IActionResult GetRequirementById(string id)
        {
            var result = _elasticSearchService.getRequirementById(id);
            return Ok(result);
        }

        // POST: requirements
        [HttpPost]
        public IActionResult createRequirement([FromBody] Requirement requirement)
        {
            var result = _elasticSearchService.createRequirement(requirement);
            return Ok();
        }

        // PUT: requirements/{id}
        [HttpPut("{id}")]
        public IActionResult updateRequirement(string id, [FromBody] Requirement requirement)
        {
            var result = _elasticSearchService.updateRequirement(id, requirement);
            return Ok(result);
        }

        // DELETE: requirements/{id}
        [HttpDelete("{id}")]
        public IActionResult deleteRequirement(string id)
        {
            var result = _elasticSearchService.deleteRequirement(id);
            return Ok(result);
        }
    }
}
