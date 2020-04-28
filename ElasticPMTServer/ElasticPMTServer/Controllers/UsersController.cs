using ElasticPMTServer.Models;
using ElasticPMTServer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var result = _userRepository.getUsers().Hits;
            return Ok(result);
        }

        // GET: users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            var result = _userRepository.getUserById(id);
            return Ok(result);
        }

        // POST: users
        [HttpPost]
        public IActionResult createUser([FromBody] User user)
        {
            var result = _userRepository.createUser(user);
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
            var result = _userRepository.updateUser(id, user);
            return Ok(result);
        }

        // DELETE: users/{id}
        [HttpDelete("{id}")]
        public IActionResult deleteUser(string id)
        {
            var result = _userRepository.deleteUser(id);
            return Ok(result);
        }
    }
}
