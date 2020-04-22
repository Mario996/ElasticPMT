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

        // GET: api/Requirements
        [HttpGet]
        public IActionResult GetAllRequirements()
        {
            var result = _elasticSearchService.getRequirements().Hits;
            return Ok(result);
        }

        // GET: api/Requirements/5
        [HttpGet("{id}")]
        public IActionResult GetRequirementById(string id)
        {
            var result = _elasticSearchService.getRequirementById(id).Hits; ;
            return Ok(result);
        }

        // POST: requirements
        [HttpPost]
        public IActionResult createRequirement([FromBody] Requirement requirement)
        {
            var result = _elasticSearchService.createRequirement(requirement);
            return Ok();
        }

        // PUT: api/Requirements/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult deleteRequirement(string id)
        {
            var result = _elasticSearchService.deleteRequirement(id);
            return Ok(result);
        }
    }
}
