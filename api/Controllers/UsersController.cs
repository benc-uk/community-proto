using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CommunityApi.Models;
using CommunityApi.Services;

namespace CommunityApi.Controlers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ICosmosDbService _cosmosDbService;

        public UsersController(ILogger<UsersController> logger, ICosmosDbService cosmosDbService)
        {
            _logger = logger;
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get([FromRoute] string id)
        {
            User user = await _cosmosDbService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            // We ignore dupes and return OK, this simplifies using the API and registering new users
            User existingUser = await _cosmosDbService.GetUserAsync(user.id);
            if (existingUser != null)
            {
                return user;
            }
            user.communities = new string[0];
            await _cosmosDbService.AddUserAsync(user);
            return user;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            // Query returns all users
            List<User> userList = await _cosmosDbService.GetUsersAsync("c.id", "@");
            return userList;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            var res = await _cosmosDbService.DeleteUserAsync(id);
            return StatusCode((int)res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update([FromRoute] string id, [FromBody] User user)
        {
            if (id != user.id)
            {
                return Problem(title: "Id in request body must match id in URL", statusCode: 400);
            }
            return await _cosmosDbService.UpdateUserAsync(id, user);
        }
    }
}
