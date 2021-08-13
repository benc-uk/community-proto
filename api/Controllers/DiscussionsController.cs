using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommunityApi.Models;
using CommunityApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CommunityApi.Controlers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscussionsController : ControllerBase
    {
        private readonly ILogger<DiscussionsController> _logger;
        private readonly CommunityDbContext _db;

        public DiscussionsController(ILogger<DiscussionsController> logger, CommunityDbContext dbContext)
        {
            _logger = logger;
            _db = dbContext;
        }

        [HttpGet("inCommunity/{communityId}")]
        public async Task<ActionResult<IEnumerable<Discussion>>> InCommunity([FromRoute] int communityId)
        {
            return await _db.Discussions.Where(d => d.Community.Id == communityId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Discussion>> Get([FromRoute] int id)
        {
            Discussion discussion = await _db.Discussions.Include(d => d.Community).Where(d => d.Id == id).FirstOrDefaultAsync();
            if (discussion == null)
            {
                return NotFound();
            }

            return discussion;
        }

        [HttpPost("inCommunity/{communityId}")]
        public async Task<ActionResult<Discussion>> Create([FromRoute] int communityId, Discussion discussion)
        {
            Community community = await _db.Communities.FindAsync(communityId);
            if (community == null)
            {
                return Problem(title: $"Community with id '{communityId}' does not exist", statusCode: 400);
            }

            _db.Discussions.Add(discussion);
            await _db.SaveChangesAsync();
            return discussion;
        }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<User>>> GetAll()
        // {
        //     List<User> userList = await _db.Users.ToListAsync();
        //     return userList;
        // }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult> Delete([FromRoute] string id)
        // {
        //     User user = await _db.Users.FindAsync(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }

        //     _db.Users.Remove(user);
        //     await _db.SaveChangesAsync();

        //     return Ok();
        // }

        // [HttpPut("{id}")]
        // public async Task<ActionResult<User>> Update([FromRoute] string id, [FromBody] User user)
        // {
        //     if (id != user.Id)
        //     {
        //         return Problem(title: "Id in request body must match id in URL", statusCode: 400);
        //     }

        //     User existingUser = await _db.Users.FindAsync(id);
        //     if (existingUser == null)
        //     {
        //         return NotFound();
        //     }

        //     existingUser.Name = user.Name;
        //     existingUser.About = user.About;
        //     existingUser.Avatar = user.Avatar;
        //     _db.Update<User>(existingUser);
        //     await _db.SaveChangesAsync();

        //     return existingUser;
        // }
    }
}
