using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommunityApi.Models;
using CommunityApi.Services;

namespace CommunityApi.Controlers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommunitiesController : ControllerBase
    {
        private readonly ILogger<CommunitiesController> _logger;
        private readonly ICosmosDbService _cosmosDbService;

        public CommunitiesController(ILogger<CommunitiesController> logger, ICosmosDbService cosmosDbService)
        {
            _logger = logger;
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Community>> Get([FromRoute] string id)
        {
            Community community = await _cosmosDbService.GetCommunityAsync(id);
            if (community == null)
            {
                return NotFound();
            }
            return community;
        }

        [HttpPost]
        public async Task<ActionResult<Community>> Create(Community community)
        {
            community.id = Guid.NewGuid().ToString();
            community.members = new string[0];
            await _cosmosDbService.AddCommunityAsync(community);
            return community;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Community>>> GetAll()
        {
            // Query returns all communities
            List<Community> communities = await _cosmosDbService.GetCommunitiesAsync("c.id", "-");
            return communities;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            var res = await _cosmosDbService.DeleteUserAsync(id);
            return StatusCode((int)res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Community>> Update([FromRoute] string id, [FromBody] Community community)
        {
            if (id != community.id)
            {
                return Problem(title: "Id in request body must match id in URL", statusCode: 400);
            }
            return await _cosmosDbService.UpdateCommunityAsync(id, community);
        }

        [HttpPut("{communityId}/join/{userId}")]
        public async Task<ActionResult> Join([FromRoute] string communityId, [FromRoute] string userId)
        {
            User user = await _cosmosDbService.GetUserAsync(userId);
            if (user == null)
            {
                return Problem(title: $"User with id '{userId}' does not exist", statusCode: 400);
            }
            Community community = await _cosmosDbService.GetCommunityAsync(communityId);
            if (community == null)
            {
                return Problem(title: $"Community with id '{communityId}' does not exist", statusCode: 400);
            }

            if (Array.Exists<string>(user.communities, id => id.Equals(communityId)))
            {
                return Problem(title: $"User '{userId}' is already a member of community '{communityId}'", statusCode: 400);
            }
            user.communities = user.communities.Append(communityId).ToArray();
            await _cosmosDbService.UpdateUserAsync(userId, user);

            community.members = community.members.Append(userId).ToArray();
            await _cosmosDbService.UpdateCommunityAsync(communityId, community);

            return Ok();
        }
    }
}
