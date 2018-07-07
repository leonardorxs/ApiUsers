using System.Collections.Generic;
using ApiUsers.Models;
using ApiUsers.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsers.Controllers
{
    [Route("api/[Controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(long id)
        {
            var user = _userRepository.Find(id);
            if (user == null)
                return NotFound();

            return new ObjectResult(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            _userRepository.Add(user);

            return CreatedAtRoute("GetUser", new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] User user)
        {
            if (user == null || user.UserId != id)
                return BadRequest();

            var userInDb = _userRepository.Find(id);

            if (userInDb == null)
                return NotFound();

            userInDb.Name = user.Name;
            userInDb.Email = user.Email;

            _userRepository.Update(userInDb);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var userInDb = _userRepository.Find(id);
            if (userInDb == null)
                return NotFound();

            _userRepository.Remove(id);
            return new NoContentResult();
        }
    }
}