using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommunityApi.Models;
using CommunityApi.Services;

namespace CommunityApi.Controlers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscussionsController : ControllerBase
    {
        private readonly ILogger<DiscussionsController> _logger;
        private readonly ICosmosDbService _cosmosDbService;

        public DiscussionsController(ILogger<DiscussionsController> logger, ICosmosDbService cosmosDbService)
        {
            _logger = logger;
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Discussion>> Get([FromRoute] string id)
        {
            Discussion Discussion = await _cosmosDbService.GetDiscussionAsync(id);
            if (Discussion == null)
            {
                return NotFound();
            }
            return Discussion;
        }

        [HttpPost]
        public async Task<ActionResult<Discussion>> Create(Discussion discussion)
        {
            Community community = await _cosmosDbService.GetCommunityAsync(discussion.community);
            if (community == null)
            {
                return Problem(title: $"Community with id '{discussion.community}' does not exist", statusCode: 400);
            }

            discussion.id = Guid.NewGuid().ToString();
            string isoDate = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
            discussion.created = isoDate;
            await _cosmosDbService.AddDiscussionAsync(discussion);
            return discussion;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            var res = await _cosmosDbService.DeleteDiscussionAsync(id);
            return StatusCode((int)res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Discussion>> Update([FromRoute] string id, [FromBody] Discussion discussion)
        {
            if (id != discussion.id)
            {
                return Problem(title: "Id in request body must match id in URL", statusCode: 400);
            }

            return await _cosmosDbService.UpdateDiscussionAsync(id, discussion);
        }
    }
}
