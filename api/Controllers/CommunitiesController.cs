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
    public class CommunitiesController : ControllerBase
    {
        private readonly ILogger<CommunitiesController> _logger;
        private readonly CommunityDbContext _db;

        public CommunitiesController(ILogger<CommunitiesController> logger, CommunityDbContext dbContext)
        {
            _logger = logger;
            _db = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Community>> Get([FromRoute] int id)
        {
            Community community = await _db.Communities.FindAsync(id);
            if (community == null)
            {
                return NotFound();
            }

            // This is probably terrible, but so is entity framework
            community.MemberCount = await _db.Users.CountAsync(u => u.Communities.Contains(community));

            return community;
        }

        [HttpPost]
        public async Task<ActionResult<Community>> Create(Community community)
        {
            _db.Communities.Add(community);
            await _db.SaveChangesAsync();
            return community;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Community>>> GetAll()
        {
            List<Community> commList = await _db.Communities.ToListAsync();
            return commList;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            Community community = await _db.Communities.FindAsync(id);
            if (community == null)
            {
                return NotFound();
            }

            _db.Communities.Remove(community);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Community>> Update([FromRoute] int id, [FromBody] Community community)
        {
            if (id != community.Id)
            {
                return Problem(title: "Id in request body must match id in URL", statusCode: 400);
            }

            Community existingComm = await _db.Communities.FindAsync(id);
            if (existingComm == null)
            {
                return NotFound();
            }

            _db.Update<Community>(community);
            await _db.SaveChangesAsync();

            return community;
        }

        [HttpPut("{communityId}/join/{userId}")]
        public async Task<ActionResult> Join([FromRoute] int communityId, [FromRoute] string userId)
        {
            User user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                return Problem(title: $"User with id '{userId}' does not exist", statusCode: 400);
            }
            Community community = await _db.Communities.FindAsync(communityId);
            if (community == null)
            {
                return Problem(title: $"Community with id '{communityId}' does not exist", statusCode: 400);
            }

            _logger.LogInformation($"User {user.Id} joined community {community.Id}");

            if (community.Members == null)
            {
                community.Members = new List<User>();
            }

            if (user.Communities == null)
            {
                user.Communities = new List<Community>();
            }
            community.Members.Add(user);
            user.Communities.Add(community);
            _db.Update<Community>(community);
            _db.Update<User>(user);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("joinedBy/{userId}")]
        public async Task<ActionResult<IEnumerable<Community>>> JoinedBy([FromRoute] string userId)
        {
            User user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                return Problem(title: $"User with id '{userId}' does not exist", statusCode: 400);
            }

            List<Community> communities = await _db.Communities.Where(c => c.Members.Contains(user)).ToListAsync();

            return communities;
        }

        [HttpGet("notJoinedBy/{userId}")]
        public async Task<ActionResult<IEnumerable<Community>>> NotJoinedBy([FromRoute] string userId)
        {
            User user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                return Problem(title: $"User with id '{userId}' does not exist", statusCode: 400);
            }

            List<Community> communities = await _db.Communities.Where(c => !c.Members.Contains(user)).ToListAsync();

            return communities;
        }

        [HttpGet("{communityId}/isMember/{userId}")]
        public async Task<ActionResult> IsMember([FromRoute] int communityId, [FromRoute] string userId)
        {
            User user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                return Problem(title: $"User with id '{userId}' does not exist", statusCode: 400);
            }
            Community community = await _db.Communities.FindAsync(communityId);
            if (community == null)
            {
                return Problem(title: $"Community with id '{communityId}' does not exist", statusCode: 400);
            }

            bool isMember = await _db.Communities.Where(c => c.Members.Contains(user)).Where(c => c.Id == communityId).AnyAsync();
            if (isMember)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
