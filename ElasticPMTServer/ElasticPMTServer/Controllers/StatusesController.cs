using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticPMTServer.Models;
using ElasticPMTServer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusRepository _statusRepository;

        public StatusesController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        // GET: statuses
        [HttpGet]
        public IActionResult GetAllStatuses()
        {
            var result = _statusRepository.getStatuses().Hits;
            return Ok(result);
        }

        // GET: statuses/{id}
        [HttpGet("{id}")]
        public IActionResult GetStatusById(string id)
        {
            var result = _statusRepository.getStatusById(id);
            return Ok(result);
        }

        // POST: statuses
        [HttpPost]
        public IActionResult createStatus([FromBody] Status status)
        {
            var result = _statusRepository.createStatus(status);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        // PUT: statuses/{id}
        [HttpPut("{id}")]
        public IActionResult updateStatus(string id, [FromBody] Status status)
        {
            var result = _statusRepository.updateStatus(id, status);
            return Ok(result);
        }

        // DELETE: statuses/{id}
        [HttpDelete("{id}")]
        public IActionResult deleteStatus(string id)
        {
            var result = _statusRepository.deleteStatus(id);
            return Ok(result);
        }
    }
}
