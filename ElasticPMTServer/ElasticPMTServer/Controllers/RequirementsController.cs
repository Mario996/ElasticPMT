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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Requirements/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: requirements
        [HttpPost]
        public IActionResult createRequirement([FromBody] Requirement requirement)
        {
            var result = _elasticSearchService.createRequirement(requirement);
            return StatusCode(result.HttpStatusCode.Value);
        }

        // PUT: api/Requirements/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
