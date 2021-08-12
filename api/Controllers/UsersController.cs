using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommunityApi.Models;
using CommunityApi.Data;
using Microsoft.EntityFrameworkCore;
//using System.Linq;

namespace CommunityApi.Controlers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly CommunityDbContext _db;

        public UsersController(ILogger<UsersController> logger, CommunityDbContext dbContext)
        {
            _logger = logger;
            _db = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get([FromRoute] string id)
        {
            User user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            // Ignore duplicate email addresses, simplifies the frontend code
            User existingUser = await _db.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                return Ok();
            }
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            List<User> userList = await _db.Users.ToListAsync();
            return userList;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            User user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update([FromRoute] string id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return Problem(title: "Id in request body must match id in URL", statusCode: 400);
            }

            User existingUser = await _db.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Name = user.Name;
            existingUser.About = user.About;
            existingUser.Avatar = user.Avatar;
            _db.Update<User>(existingUser);
            await _db.SaveChangesAsync();

            return existingUser;
        }
    }
}
