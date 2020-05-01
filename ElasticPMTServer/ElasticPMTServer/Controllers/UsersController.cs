using ElasticPMTServer.Models;
using ElasticPMTServer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UsersController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var result = _userRepository.getAll().Hits;
            return Ok(result);
        }

        // GET: users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            var result = _userRepository.getById(id);
            return Ok(result);
        }

        // POST: users
        [HttpPost]
        public IActionResult createUser([FromBody] User user)
        {
            var result = _userRepository.create(user);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        // PUT: users/{id}
        [HttpPut("{id}")]
        public IActionResult updateUser(string id, [FromBody] User user)
        {
            var result = _userRepository.update(id, user);
            return Ok(result);
        }

        // DELETE: users/{id}
        [HttpDelete("{id}")]
        public IActionResult deleteUser(string id)
        {
            var result = _userRepository.delete(id);
            return Ok(result);
        }
    }
}
